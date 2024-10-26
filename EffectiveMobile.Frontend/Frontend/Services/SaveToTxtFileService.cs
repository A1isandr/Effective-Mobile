using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Frontend.Models;
using Frontend.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Frontend.Helpers;
using Microsoft.Extensions.Hosting;

namespace Frontend.Services
{
    public class SaveToTxtFileService : ISaveToFileService
    {
        private readonly ILogger _logger;
        private readonly string _defaultSavePath;

        public SaveToTxtFileService(ILogger logger)
        {
            _logger = logger;
            _defaultSavePath = PathHelper.GetFullPath(ConfigurationManager.AppSettings["DefaultSavePath"]!);
        }

        /// <inheritdoc/>
        public async Task SaveAsync(IEnumerable<Order> orders)
        {
            try
            {
                var file = await GetFileAsync();

                if (file is not null)
                {
                    _logger.Information($"Saving orders to a text file: {file.Path}");

                    await using var stream = await file.OpenWriteAsync();
                    await using var streamWriter = new StreamWriter(stream);

                    foreach (var order in orders)
                    {
                        await streamWriter.WriteLineAsync($"{order.Id} {order.Weight} {order.DistrictId} {order.DueTime}");
                    }

                    _logger.Information("Orders saved successfully.");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to save orders.");
                throw;
            }
        }

        private async Task<IStorageFile?> GetFileAsync()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                desktop.MainWindow?.StorageProvider is not { } provider)
            {
                throw new NullReferenceException("Missing StorageProvider instance.");
            }

            return await provider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Save to a text file.",
                FileTypeChoices = [FilePickerFileTypes.TextPlain],
                DefaultExtension = ".txt",
                SuggestedFileName = $"Orders {DateTime.Now.Date:yy-MM-dd} {(int)DateTime.Now.TimeOfDay.TotalSeconds}",
                SuggestedStartLocation = await provider.TryGetFolderFromPathAsync(
                    Directory.CreateDirectory(_defaultSavePath).FullName)
            });
        }
    }
}

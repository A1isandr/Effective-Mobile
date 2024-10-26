using System.Configuration;
using System.IO;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Interfaces;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SukiUI.Toasts;

namespace Frontend.Common
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IOrdersService, OrdersService>();
            services.AddSingleton<ISukiToastManager, SukiToastManager>();

            services.AddTransient<ISaveToFileService, SaveToTxtFileService>();

            services.AddHttpClient();

            services.AddSerilog((_, loggerConfiguration) => loggerConfiguration
                .Enrich.FromLogContext()
                .WriteTo.File(
                    Directory.CreateDirectory(
                        PathHelper.GetFullPath(ConfigurationManager.AppSettings["DefaultLogPath"]!)) + "\\log.txt",
                    shared: true, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 5));
        }
    }
}

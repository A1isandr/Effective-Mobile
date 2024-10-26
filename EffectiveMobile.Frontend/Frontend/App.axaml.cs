using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Frontend.Common;
using Frontend.Helpers;
using Frontend.ViewModels;
using Frontend.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Frontend;

public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        InitializeDependencyInjection();
    }

    private static void InitializeDependencyInjection()
    {
        var collection = new ServiceCollection();
        collection.AddCommonServices();
        ServiceProvider = collection.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

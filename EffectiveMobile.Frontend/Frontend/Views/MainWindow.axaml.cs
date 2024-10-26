using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using SukiUI.Toasts;

namespace Frontend.Views;

public partial class MainWindow : SukiWindow
{
    public MainWindow()
    {
        InitializeComponent();

        ToastHost.Manager = App.ServiceProvider.GetRequiredService<ISukiToastManager>();
    }
}

using Avalonia.Headless;
using Avalonia.Headless.NUnit;
using Avalonia.Input;
using Frontend.ViewModels;
using Frontend.Views;
using SukiUI.Toasts;

namespace Frontend.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [AvaloniaTest]
        public void Test1()
        {
            var main = new MainView
            {
                DataContext = new MainViewModel(new OrdersService(), new SaveToTxtFileService(), new SukiToastManager())
            };

            var window = new MainWindow
            {
                Content = main
            };

            window.Show();

            main.RefreshButton.Focus();
            window.KeyPress(Key.Enter, RawInputModifiers.None, PhysicalKey.Enter, null);

            Assert.That(main.Data.Columns, Has.Count.EqualTo(3));
        }
    }
}
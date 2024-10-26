using Avalonia;
using Avalonia.Headless;
using Frontend.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace Frontend.Test
{
    public class TestAppBuilder
    {
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions())
            .UseReactiveUI();
    }

}

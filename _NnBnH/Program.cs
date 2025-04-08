using _NnBnH.Views.UIcontrols;
using Avalonia;
using Avalonia.Threading;
using MsBox.Avalonia.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _NnBnH;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        //AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        //{
        //    if (e.ExceptionObject is Exception ex)
        //    {
        //        Dispatcher.UIThread.InvokeAsync(() =>
        //            new MessageBoxWindow("CS exception has happen", ex.Message + $"\n\n{DateTime.Now}").Show()
        //        );
        //    }
        //};

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}

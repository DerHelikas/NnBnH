using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using _NnBnH.ViewModels;
using _NnBnH.Views;

namespace _NnBnH;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            var SplashScreenWindow = new SplashScreenWindow();
            {
                DataContext = new SplashScreenWindowViewModel();
            }
            ;
            MainWindow MainWin = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };


            desktop.MainWindow = SplashScreenWindow;
            SplashScreenWindow.LoadingIsFinishedEvent += () =>
            {
                MainWin.Show();
                desktop.MainWindow = MainWin;

                SplashScreenWindow.Close();
            };
            SplashScreenWindow.Show();


        }

        base.OnFrameworkInitializationCompleted();
    }
}
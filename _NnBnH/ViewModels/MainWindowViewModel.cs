using _NnBnH.MainNnBnH.RuntimeElements;
using _NnBnH.MainNnBnH.SettingsClass;
using _NnBnH.Views;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace _NnBnH.ViewModels;

//#pragma warning disable CA1822 // Mark members as static
//#pragma warning restore CA1822 // Mark members as static
public partial class MainWindowViewModel : ViewModelBase
{

    [ObservableProperty]
    private ViewModelBase _currentPage = new MainPageUserControlViewModel();

    [ObservableProperty]
    private bool _isAutonomeMode = DuringRuntimeVariables.IsAutonomusMode;

    public string VersionLabel { get; } =
        $"\"{ApplicationVersionData.VersionCode}\" :" +
        $"{ApplicationVersionData.Version[0]}." +
        $"{ApplicationVersionData.Version[1]}." +
        $"{ApplicationVersionData.Version[2]}";

    public MainWindowViewModel()
    {
        _isAutonomeMode = !DuringRuntimeVariables.IsAutonomusMode;
    }
    


    public void ShowMainPage()
    {
        CurrentPage = (ViewModelBase)new MainPageUserControlViewModel();
    }

    public void ShowKanaMenu()
    {
        CurrentPage = (ViewModelBase)new KanaDisplayViewModel();
    }

    public void ShowKanaPracticeMenu()
    {
        CurrentPage = (ViewModelBase)new KanaPracticeViewModel("32", new string[] { });
    }

    public void ShowKanjiMenu()
    {
        CurrentPage = new KanjiDisplayViewModel();
    }
}


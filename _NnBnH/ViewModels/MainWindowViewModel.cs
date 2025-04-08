
using _NnBnH.MainNnBnH;
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
    private bool _isAutonomeMode = !DuringRuntimeVars.IsAutonomusMode;

    public string VersionLabel { get; } =
        $"\"{_NnBnH.MainNnBnH.ApplicationVersionData.VersionCode}\" :" +
        $"{MainNnBnH.ApplicationVersionData.Version[0]}." +
        $"{MainNnBnH.ApplicationVersionData.Version[1]}." +
        $"{MainNnBnH.ApplicationVersionData.Version[2]}";

    public MainWindowViewModel()
    {
        _isAutonomeMode = DuringRuntimeVars.IsAutonomusMode;
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


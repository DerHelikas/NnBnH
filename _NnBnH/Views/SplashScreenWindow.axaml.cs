using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using _NnBnH.MainNnBnH.SettingsClass;
using System.Threading.Tasks;
using MsBox.Avalonia;
using _NnBnH.MainNnBnH.Actions;
using _NnBnH.MainNnBnH.RuntimeElements;

namespace _NnBnH.Views;

public partial class SplashScreenWindow : Window
{

    private bool _isInitialized = false;

    public SplashScreenWindow()
    {
        InitializeComponent();
        this.Activated += async (o, e) => await LoadProgram(o, e);
    }

    public event Action? LoadingIsFinishedEvent;
    public async Task LoadProgram(object o, EventArgs a)
    {
        //TODO:Create something that will block a second attempt to begin "Load Program"
        if (!_isInitialized)
            _isInitialized = true;
        else return;

        bool showMsBoxes = Design.IsDesignMode ? false : true;

        this.TasksLog.Text = string.Empty;
        this.TasksLog.Text += "╭─Loading Settings...\n";
        Exception ex = null;
        _NnBnH.MainNnBnH.SettingsClass.ProgramSettings.LoadSettings(out ex);
        this.ProgressBar.Value += 15;
        this.TasksLog.Text += "╰─Finshed\n";


        //+15
        await CheckWebAPI(showMsBoxes);
        //+15

        MainNnBnH.AssetsMaintaining.CommonAssetsController controller =
        new MainNnBnH.AssetsMaintaining.CommonAssetsController();

        //(out exceptionsFromLoad);
        System.Collections.Generic.List<Exception> exceptionsFromLoad = new System.Collections.Generic.List<Exception>();
        await controller.CheckAndLoadAll();

        this.ProgressBar.Value += exceptionsFromLoad.Count * 15;

        LoadingIsFinishedEvent?.Invoke();
    }


    /// <summary>
    /// Checks web api
    /// </summary>
    /// <param name="showMsBoxes"></param>
    private async Task CheckWebAPI(bool showMsBoxes = true)
    {

        while (true)
        {
            this.TasksLog.Text += "╭─Trying to get status of WebAPI\n";
            try
            {

                string resp = await
                     MainNnBnH.ExternalProviders.WebAPI.WebAPIconnection.HttpProvider
                     .GetStringAsync(new Uri(ProgramSettings.WebApi_Uri, "api/Status"));

                this.TasksLog.Text = "╰─" + resp;

                if (resp != "OnLine")
                    throw new Exception("API behaves weird: " + resp);

                else break;
            }
            catch (Exception ex)
            {
                this.TasksLog.Text += $"╰─{ex.Message}\n";

                if (showMsBoxes)
                {

                    var b = MessageBoxManager.GetMessageBoxStandard
                              (
                             "Web API error",
                              $"{ex.Message}\n" +
                              "Go into the Autonomus mode?\n" +
                              "All your progress will not be memorized.\n" +
                              "Cancel - exit from the application\n" +
                              "No - Retry",
                              MsBox.Avalonia.Enums.ButtonEnum.YesNoCancel,
                              MsBox.Avalonia.Enums.Icon.Wifi);

                    var res = await b.ShowAsync();

                    if (res == MsBox.Avalonia.Enums.ButtonResult.Yes)
                    {
                        DuringRuntimeVariables.IsAutonomusMode = true;
                        break;
                    }
                    else if (res == MsBox.Avalonia.Enums.ButtonResult.Cancel)
                    {
                        ApplicationActions.ApplicationExit();
                    }
                }
            }

        }
        this.ProgressBar.Value += 15;
    }
}
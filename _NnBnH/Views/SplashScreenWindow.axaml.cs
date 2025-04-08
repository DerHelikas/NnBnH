using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;

using _NnBnH.MainNnBnH;
using _NnBnH.MainNnBnH.SettingsClass;
using System.Threading.Tasks;
using MsBox.Avalonia;

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
        _NnBnH.MainNnBnH.SettingsClass.ProgramSettings.LoadSettings();
        this.ProgressBar.Value += 15;
        this.TasksLog.Text += "╰─Finshed\n";


        //+15
        await CheckWebAPI(showMsBoxes);
        //+15

        System.Collections.Generic.List<Exception> exceptionsFromLoad = new System.Collections.Generic.List<Exception>();
        _NnBnH.MainNnBnH.ExternalProviders.PreloadedAssets.PreloadedAssetsTryLoad(out exceptionsFromLoad);

        this.ProgressBar.Value += exceptionsFromLoad.Count * 15;

        LoadingIsFinishedEvent?.Invoke();
    }



    /// <summary>
    /// Checks web api
    /// </summary>
    /// <param name="showMsBoxes"></param>
    private async Task CheckWebAPI(bool showMsBoxes = true)
    {
        this.TasksLog.Text += "╭─Trying to get status of WebAPI\n";
        try
        {

            string resp = await
                 MainNnBnH.ExternalProviders.WebAPI.WebAPIconnection.HttpProvider
                 .GetStringAsync(ProgramSettings.WebApiUri);

            this.TasksLog.Text = "╰─" + resp;

            if (resp != "OnLine")
                throw new Exception("API behaves weird: " + resp);
        }
        catch (Exception ex)
        {
            this.TasksLog.Text += $"╰─{ex.Message}\n";

            if (showMsBoxes)
            {

                //MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard("", "");
                var b = MessageBoxManager.GetMessageBoxStandard
                          (
                         "Web API error",
                           ex.Message
                          + "\n Go into the Autonomus mode?\n All your progress will not be memorized.\n" +
                          "No - exit from the application",
                          MsBox.Avalonia.Enums.ButtonEnum.YesNo,
                          MsBox.Avalonia.Enums.Icon.Wifi);

                var res = await b.ShowAsync();

                if (res == MsBox.Avalonia.Enums.ButtonResult.Yes)
                {
                    DuringRuntimeVars.IsAutonomusMode = true;
                }
                else
                {
                    ApplicationActions.ApplicationExit();
                }
            }

        }
        this.ProgressBar.Value += 15;
    }
}
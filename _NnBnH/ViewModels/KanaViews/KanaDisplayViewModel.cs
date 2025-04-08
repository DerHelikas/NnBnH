using _NnBnH.Models;
using _NnBnH.MainNnBnH.ExternalProviders.DB;
using _NnBnH.MainNnBnH.Functions.CSadditions;
using _NnBnH.MainNnBnH.SettingsClass;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace _NnBnH.ViewModels
{
    internal partial class KanaDisplayViewModel : ViewModelBase
    {
        public DB_PostgreSQLconnectionClass KanaConnection { get; set; }


        [ObservableProperty]
        private ObservableCollection<KanaGroupedLine> _kanaHolder = new ObservableCollection<KanaGroupedLine>();

        [ObservableProperty]
        private bool _showTaskBar = false, _showProgressBar = true;

        [ObservableProperty]
        private string _taskBarState = "State example";

        [ObservableProperty]
        private int _taskBarPercent = 0;


        public async Task LoadKanaFromDB()
        {
            await Task.Run(() =>
            {

                Exception ex;
                KanaConnection?.LondAllKanaFromDB(ProgramSettings.KanaTable_Name, out ex);

            });


        }

        public void ReloadKanaTable()
        {
            ShowTaskBar = ShowProgressBar = true;
            TaskBarPercent = 0;
            TaskBarState = "Obtaining Kana table from DB";

            if (KanaConnection?.ConnectionString != string.Empty)
                LoadKanaFromDB();

        }


        public KanaDisplayViewModel()
        {
            KanaHolder = new ObservableCollection<KanaGroupedLine>();



            // Since cfg file is not present in cod, Avalonia Compile can't predict existance of DBconnectionString
            // in the settings Dictionary
            // Due of that, Any external connections will be ignored in Design mode.
            if (Design.IsDesignMode)
                return;
            // All next code will be ignored in DesignMode. 

            KanaHolder = new ObservableCollection<KanaGroupedLine>(MainNnBnH.DuringRuntimeVars.ActuallKanaTable);

            ReloadKanaTable();
        }

        private void KanaConnection_LoadedKanaEvent(int KanaCount, DB_ObjectFetchedEventArgs dB_ObjectFetchedEventArgs)
        {
            if (dB_ObjectFetchedEventArgs.failed)
            {
                TaskBarPercent = 0;
                ShowProgressBar = false;
                TaskBarState = dB_ObjectFetchedEventArgs.exception?.Message ?? "";

                return;
            }

            ShowTaskBar = false;
            TaskBarState = "Finished";
            KanaHolder = new ObservableCollection<KanaGroupedLine>(dB_ObjectFetchedEventArgs.Return as List<KanaGroupedLine>);
        }

        private void KanaConnection_KanaLoadingPercentChangedEvent(int Percent, int Previous, int Step)
        {
            TaskBarPercent = Percent;
        }

    }
}
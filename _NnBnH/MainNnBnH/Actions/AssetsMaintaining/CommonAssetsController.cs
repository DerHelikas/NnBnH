using _NnBnH.MainNnBnH.RuntimeElements;
using _NnBnH.MainNnBnH.SettingsClass;
using _NnBnH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.AssetsMaintaining
{
    /// <summary>
    /// Controls the whol Assets (not Avalonia Assets) of the program.
    /// <para>Communicates with <see cref="_NnBnH.MainNnBnH.AssetsMaintaining.PreloadedAssets"/></para>
    /// <para><see cref="_NnBnH.MainNnBnH.ExternalProviders.WebAPI.WebAPIconnection"/></para>
    /// <para><see cref="_NnBnH.MainNnBnH.ExternalProviders.DB.DB_CommonDBConnectionClass"/></para>
    /// </summary>
    public partial class CommonAssetsController
    {

        public event Action<Exception>? ExceptionHappenedEvent;
        public event Action<int, string>? PhasePassed;


        /// <summary>
        /// Checks all parts of assets in the program, and then downloads it
        /// if they don't exist in <see cref="PreloadedAssets"/> if this function is on.
        /// 
        /// <para>It tries to load data into DVR <see cref="DuringRuntimeVariables"/></para>
        /// </summary>
        public async Task CheckAndLoadAll()
        {
            //KanaTable
            Exception ex;

            bool ResaveKanaTable = false;
            if (ProgramSettings.PreloadedAssets_IsInUse)
            {

                List<KanaGroupedLine> KanaTable = PreloadedAssets.PreloadedAssetsTryLoadKana(out ex);
                if (ex == null && KanaTable != null)
                {
                    DuringRuntimeVariables.ChangeKanaTable(KanaTable);
                }
                else if (ex.GetType() == typeof(FileNotFoundException))
                {
                    ResaveKanaTable = true;
                    await ExternalLoadKanaTable();
                }
                else this.ExceptionHappenedEvent?.Invoke(ex);

                if (ResaveKanaTable)
                {
                    PreloadedAssets.PreloadedAssets_SaveDRV(out ex);
                }
                
                return;
            }





        }

        public async Task ExternalLoadKanaTable()
        {
            List<KanaGroupedLine> kanatable = new List<KanaGroupedLine>();
            if (MainNnBnH.SettingsClass.ProgramSettings.Common_DataSource == "webapi")
            {
                string kanatableSerialized = await MainNnBnH.ExternalProviders.WebAPI.WebAPIconnection.HttpProvider
                 .GetStringAsync(
                     new Uri(SettingsClass.ProgramSettings.WebApi_Uri, "api/Kana"));
                if (kanatableSerialized != null)
                {
                    kanatable = JsonConvert.DeserializeObject<List<KanaGroupedLine>>(kanatableSerialized)
                        ?? throw new NullReferenceException();

                    DuringRuntimeVariables.ChangeKanaTable(kanatable);
                }
            }

            // TODO: Add DB support


        }

    }
}

using _NnBnH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
using _NnBnH.MainNnBnH.SettingsClass;
using _NnBnH.MainNnBnH.RuntimeElements;

namespace _NnBnH.MainNnBnH.AssetsMaintaining
{
    /// <summary>
    /// Class that defines methods to work with 
    /// already loaded assets into the DRV <see cref="DuringRuntimeVariables"/>.
    /// </summary>
    public static class PreloadedAssets
    {
        /// <summary>
        /// Common method to try load all data 
        /// into DRV <see cref="DuringRuntimeVariables"/>
        /// </summary>
        /// <param name="exceptions"></param>
        public static void PreloadedAssetsTryLoad(out List<Exception> exceptions)
        {
            exceptions = new List<Exception>();

            Exception commonCollector;

            PreloadedAssetsTryLoadKana(out commonCollector);
            exceptions.Add(commonCollector);

        }


        /// <summary>
        /// Searches for defined in Settings ProgramSettings.PreloadedAssets_KanaTable
        /// file. If it exist, then it deserialize it into an object. 
        /// </summary>
        /// <param name="ex">If no exception had happened, then ex = null</param>
        /// <returns>Kana table</returns>
        public static List<KanaGroupedLine>? PreloadedAssetsTryLoadKana(out Exception ex)
        {

            string path = Path.Combine(ProgramSettings.PreloadedAssets_Folder, ProgramSettings.PreloadedAssets_KanaTable);
            if (!File.Exists(path))
            {
                ex = new FileNotFoundException("Kana table does not exist");

                return null;
            }

            string fileData = File.ReadAllText(path);

            try
            {

                ex = null;
                return JsonConvert.DeserializeObject<List<KanaGroupedLine>>(fileData);
            }
            catch (Exception exc)
            {
                ex = exc;

                return null;
            }
        }

        /// <summary>
        /// Saves planned DVR veriables <see cref="DuringRuntimeVariables"/> 
        /// into their json files
        /// </summary>
        /// <param name="ex">Exception during the execution</param>
        public static void PreloadedAssets_SaveDRV(out Exception ex)
        {
            if (!Directory.Exists(ProgramSettings.PreloadedAssets_Folder))
                Directory.CreateDirectory(ProgramSettings.PreloadedAssets_Folder);


            //---- KanaTable
            string json = JsonConvert.SerializeObject(DuringRuntimeVariables.ActuallKanaTable, Formatting.Indented);
            string path = Path.Combine(ProgramSettings.PreloadedAssets_Folder, ProgramSettings.PreloadedAssets_KanaTable);
            File.WriteAllText(path, json);
            if (!File.Exists(path))
            {
                ex = new FileNotFoundException("Can't save kana table");
                return;
            }
            //-------------


            ex = default;
        }

    }
}

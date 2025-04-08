using _NnBnH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
using _NnBnH.MainNnBnH.SettingsClass;

namespace _NnBnH.MainNnBnH.ExternalProviders
{
    /// <summary>
    /// Class that defines methods to work with 
    /// already loaded assets into the DRV <see cref="DuringRuntimeVars"/>.
    /// </summary>
    public static class PreloadedAssets
    {
        /// <summary>
        /// Common method to try load all data 
        /// into DRV <see cref="DuringRuntimeVars"/>
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

            if (!File.Exists(_NnBnH.MainNnBnH.SettingsClass.ProgramSettings.PreloadedAssets_KanaTable))
            {
                ex = new FileNotFoundException("Kana table does not exist");

                return null;
            }

            string fileData = File.ReadAllText(ProgramSettings.PreloadedAssets_KanaTable);

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

    }
}

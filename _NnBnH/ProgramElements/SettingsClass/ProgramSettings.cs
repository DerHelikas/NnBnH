using _NnBnH.MainNnBnH.Attributes.SettingsAtributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.SettingsClass
{
    public static class ProgramSettings
    {
        public static bool PreloadedAssets_IsInUse = true;
        public static string PreloadedAssets_Folder = "./Preloaded/";
        public static string PreloadedAssets_KanaTable = "Kana.json";
        public static string PreloadedAssets_CommonKanjiTable = "Kanji.json";

        public static string Common_DBconnectionString = string.Empty;
        public static string Common_DataSource = string.Empty;

        public static string KanaTable_DataSource = string.Empty;
        public static string KanaTable_DBconnectionString = string.Empty;
        public static string KanaTable_Name = string.Empty;

        public static string KanjiTableName = string.Empty;


        public static string ConfigFileName = "";

        [NonSerialisableSettingField]
        public static readonly string __ConstantConfigFileName = "Settings.cfg";

        [NonSerialisableSettingField]
        public static readonly string __ConstantConfigFilesDirectory = "./Settings/";

        [NonSerialisableSettingField]
        public static readonly System.Uri WebApiUri;


        public static void SaveSettings()
        {
            FieldInfo[] ProgramSettings =
                typeof(ProgramSettings)
                .GetFields()
                .Where(field => !field.CustomAttributes.Any(elm => elm.AttributeType.Name == "NonSerialisableSettingFieldAttribute"))
                .ToArray();

            string CurrentConfigFileName = ConfigFileName;
            if (ConfigFileName == string.Empty)
                CurrentConfigFileName = Path.Combine(__ConstantConfigFilesDirectory, __ConstantConfigFileName);

            if (!Directory.Exists(__ConstantConfigFilesDirectory))
                Directory.CreateDirectory(__ConstantConfigFilesDirectory);


            string data = string.Empty;
            foreach (var item in ProgramSettings)
            {
                data += $"{item.Name} = \"{item.GetValue(null)}\"\r\n";
            }
            File.WriteAllText(CurrentConfigFileName, data);
        }

        public static void LoadSettings()
        {
            FieldInfo[] ProgramSettings =
                typeof(ProgramSettings)
                .GetFields()
                .Where(field => !field.CustomAttributes.Any(elm => elm.AttributeType.Name == "NonSerialisableSettingFieldAttribute"))
                .ToArray();
        }
    }

}

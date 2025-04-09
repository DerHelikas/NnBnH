using _NnBnH.MainNnBnH.CodeElements.Attributes;
using _NnBnH.MainNnBnH.CodeElements.Exceptions.ConfigExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace _NnBnH.MainNnBnH.SettingsClass
{
    public static class ProgramSettings
    {
        public static bool PreloadedAssets_IsInUse = true;
        public static string PreloadedAssets_Folder = "./Preloaded/";
        public static string PreloadedAssets_KanaTable = "Kana.json";
        public static string PreloadedAssets_CommonKanjiTable = "Kanji.json";

        public static string Common_DataSource = string.Empty;

        public static string Common_DB_ConnectionString = string.Empty;

        public static string KanaTable_DB_connectionString = string.Empty;
        public static string KanaTable_DB_Name = string.Empty;
        public static string KanjiTable_DB_Name = string.Empty;

        public static bool Account_IsInUse = false;

        public static System.Uri WebApi_Uri = null;


        [NonSerialisableSettingField]
        public static readonly string __ConstantConfigFileName = "Settings.cfg";

        [NonSerialisableSettingField]
        public static readonly string __ConstantConfigFilesDirectory = "./Settings/";


        private static string GetCombinedConfigFilePath { get => Path.Combine(__ConstantConfigFilesDirectory, __ConstantConfigFileName); }

        [NonSerialisableSettingField]
        private static readonly string StandartCFGfileRegexchexk = @"^(\S+).*=.*""(.*)""";

        public static void SaveSettings()
        {
            FieldInfo[] ProgramSettings =
                typeof(ProgramSettings)
                .GetFields()
                .Where(field => !field.CustomAttributes.Any(elm => elm.AttributeType.Name == "NonSerialisableSettingFieldAttribute"))
                .ToArray();



            if (!Directory.Exists(__ConstantConfigFilesDirectory))
                Directory.CreateDirectory(__ConstantConfigFilesDirectory);


            string data = string.Empty;
            foreach (var item in ProgramSettings)
            {
                data += $"{item.Name} = \"{item.GetValue(null)}\"\r\n";
            }
            File.WriteAllText(GetCombinedConfigFilePath, data);
        }


        /// <summary>
        /// Recreates static config file in the defult location
        /// </summary>
        /// <param name="ReWrite">if Set true, thne alredy existing fille will be recreated</param>
        public static void CreateDefult(bool ReWrite = false)
        {
            //TODO: Add defult  actions
            SaveSettings();
        }

        /// <summary>
        /// Loads data from config file 
        /// </summary>
        /// TODO: Add support for custom paths and names
        public static void LoadSettings(out Exception ExternalException)
        {
            FieldInfo[] ProgramSettings =
                typeof(ProgramSettings)
                .GetFields()
                .Where(field => !field.CustomAttributes.Any(elm => elm.AttributeType.Name == "NonSerialisableSettingFieldAttribute"))
                .ToArray();

            try
            {

                List<string> data = File.ReadAllLines(GetCombinedConfigFilePath)
                    .ToList()
                    .Where(field => !(string.IsNullOrWhiteSpace(field) && string.IsNullOrEmpty(field)))
                    .ToList();

                //verifing
                if (data.Count == 0 || data.All(a => string.IsNullOrEmpty(a)))
                    throw new ConfigFileIsEmptyException();

                for (int i = 0; i < data.Count; i++)
                {
                    if (!Regex.IsMatch(data[i], StandartCFGfileRegexchexk))
                        throw new ConfigBrokenLineException(i);
                }
                //end verifing.

                foreach (var field in ProgramSettings)
                {
                    string foundField =
                        data.Find(filedname => (filedname.Split('=')[0].Trim()) == field.Name);

                    string value = Regex.Match(foundField, StandartCFGfileRegexchexk).Groups[2].Value;

                    if (value != null)
                    {
                        Type type = field.FieldType;

                        switch (type.FullName)
                        {
                            case "System.Boolean":
                                bool content = false;
                                if (bool.TryParse(value, out content))
                                {
                                    field.SetValue(type, content);
                                }
                                else
                                {
                                    throw new InvalidCastException($"Can't cast {type.FullName} of field {field}");
                                }
                                break;

                            case "System.String":
                                field.SetValue(type, value);
                                break;

                            case "System.Uri":
                                field.SetValue(type, new Uri(value));
                                break;

                        }
                    }
                }

            }
            catch (Exception ex) when
            (
            ex is FileNotFoundException ||
            ex is ArgumentException ||
            ex is PathTooLongException
            )
            {
                CreateDefult(false);
                ExternalException = new ConfigFilePathBrokenException();
                return;
            }
            catch (Exception ex)
            {
                ExternalException = ex;
                return;
            }


            ExternalException = null;
        }
    }

}

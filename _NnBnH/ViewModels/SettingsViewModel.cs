using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace _NnBnH.ViewModels;
    public partial class SettingsViewModel : ViewModelBase
    {

        [ObservableProperty]
        private List<Tuple<string,string>> _fromSettingsData = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>(string.Empty,string.Empty)
        };

        public SettingsViewModel()
        {
            _fromSettingsData = new List<Tuple<string, string>>();

            FieldInfo[] SettingsFieldsinfo = typeof(_NnBnH.MainNnBnH.SettingsClass.ProgramSettings).GetFields();

            foreach (var item in SettingsFieldsinfo)
            {
                string val = item.GetValue(null).ToString();
                if (val == string.Empty || string.IsNullOrWhiteSpace(val))
                    val = "Not stated";
                _fromSettingsData.Add(new Tuple<string, string>(item.Name, val));
            }

        }
    }

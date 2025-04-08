using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.ViewModels
{
    internal partial class KanaPracticeViewModel : ViewModelBase
    {
            

        [ObservableProperty]
        private string _currentShow = "を";


        [ObservableProperty]
        private string _key1Bind  = "Q";

        [ObservableProperty]
        private string _key2Bind  = "W";

        [ObservableProperty]
        private string _key3Bind  = "E";

        [ObservableProperty]
        private string _key4Bind  = "R";

        [ObservableProperty]
        private int 
            _currentCorrectCounter = 0, 
            _currentIncorrectCounter = 0,
            _currentAllAnswers = 0;

        public int Corrects { get; set; } = 0;

        public KanaPracticeViewModel()
        {

        }

        public KanaPracticeViewModel(string previousPageName, string[] SelectedKana)
        {

        }

        public void Check(string msg)
        {
            CurrentShow = msg;
        }
        public void Shuffle()
        {


        }
    }
}

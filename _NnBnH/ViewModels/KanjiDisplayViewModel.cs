using _NnBnH.Models;
using _NnBnH.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _NnBnH.ViewModels;
    public partial class KanjiDisplayViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _showTaskBar = false, _showProgressBar = true;

        [ObservableProperty]
        private string _taskBarState = "State example";

        [ObservableProperty]
        private int _taskBarPercent = 0;

        public List<Kanji> Kanjis = new List<Kanji>()
        {
            new Kanji("日",new KanjiReading(new string[] {"にち" }, new string[] {"ニ" }))
            {
                Meanings = new string[]{ "sun", "day" }
            },
            new Kanji("本", new KanjiReading(new string[] { "ほん" }, new string[] { "ホン" }))
            {
                Meanings = new string[] { "Book", "Source", "Main"},
                Keys = new string[] { "木" }
            },
            new Kanji("私", new KanjiReading(new string[] { "わたし" }, new string[] { "-" }))

        };


        public ObservableCollection<Kanji> KanjiGUI { get; set; } = new ObservableCollection<Kanji>();

        public void PrintKanjiShowWindow()
        {
            new KanjImagingWindow(Kanjis).Show();
        }

        public KanjiDisplayViewModel()
        {
            KanjiGUI = new ObservableCollection<Kanji>(Kanjis);
        }


    }

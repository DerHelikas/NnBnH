using _NnBnH.Models;
using _NnBnH.MainNnBnH.ProgramOut;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _NnBnH.ViewModels;
    public partial class KanjImagingWindowViewModel : ViewModelBase
    {

        private List<Kanji> currentKanjiTable = new List<Kanji>();
        private Imaging imaging = new Imaging();

        [ObservableProperty]
        private int _plannedPages = 0, _processedPages = 0;

        [ObservableProperty]
        private Size _currentBageWH = new Size(0);

        [ObservableProperty]
        private string _formattedPageWH = string.Empty;

        [ObservableProperty]
        private Bitmap _imageSrc;

        public KanjImagingWindowViewModel()
        {

        }
        public KanjImagingWindowViewModel(List<Kanji> KanjiTable)
        {
            this.currentKanjiTable = KanjiTable;
            this.imaging = new Imaging();

            this.imaging.DocumentIsCalculatedEvent += Imaging_DocumentIsCalculatedEvent;
            this.imaging.PageReadyEvent += Imaging_PageReadyEvent;
            this.imaging.DocumentIsReadyEvent += Imaging_DocumentIsReadyEvent;
        }

        private void Imaging_DocumentIsReadyEvent(int PagesCount)
        {
            ImageSrc = null;
            ProcessedPages = 0;
            CurrentBageWH = new Size(0, 0);
            FormattedPageWH = "Finished";
        }

        private void Imaging_DocumentIsCalculatedEvent(int PagesCount)
        {
            PlannedPages = PagesCount;
            ProcessedPages = 0;
            CurrentBageWH = new Size(0);
            FormattedPageWH = string.Empty;
        }

        private void Imaging_PageReadyEvent(Imaging.PageEventInfo page)
        {
            ProcessedPages++;
            CurrentBageWH = page.WH;
            FormattedPageWH = $"{page.WH.Width}x{page.WH.Height}";

            using (MemoryStream memory = new MemoryStream())
            {
                page.refImage.Save(memory, JpegFormat.Instance);
                memory.Position = 0;
                ImageSrc = new Bitmap(memory);
            }



        }

        public async Task PrintButton()
        {
            await Task.Run(() =>
            {
                imaging.CompileKanjiDictionary(currentKanjiTable);
            });
        }

    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _NnBnH.Models;
using Avalonia.Media;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;
/*
    USED 
        SixLabors.ImageSharp 
        SixLabors.Fonts
        SixLabors.ImageSharp.Drawing

    under Apache 2 license, since
    the project published under freeware licenses
 */

namespace _NnBnH.MainNnBnH.ProgramOut
{
    /// <summary>
    /// Represents a set of options that allows create Sheets(Tables) of such
    /// elements as Kana, Kanji and etcetera
    /// </summary>
    public class Imaging
    {
     
        public class PageEventInfo
        {
            public PageEventInfo(int pageId, Size wH, Image<Rgba32> refImage)
            {
                PageId = pageId;
                WH = wH;
                this.refImage = refImage;
            }

            public int PageId { get; }
            public Size WH { get; }

            public Image<Rgba32> refImage { get; }
        }


        /// <summary>
        /// Declares a delegate void for <see cref="PageReadyEvent"/>
        /// </summary>
        /// <param name="page"></param>
        public delegate void PageReadyDelegate(PageEventInfo page);
        /// <summary>
        /// Happens when page is ready and added to the document
        /// </summary>
        public event PageReadyDelegate? PageReadyEvent;



        /// <summary>
        /// Declares a delegate void for <see cref="DocumentIsReadyEvent"/>
        /// </summary>
        /// <param name="PagesCount"></param>
        public delegate void DocumentIsReady(int PagesCount);
        /// <summary>
        /// Happens when all pages are printed into the document
        /// </summary>
        public event DocumentIsReady? DocumentIsReadyEvent;


        /// <summary>
        /// Declares a delegate void for <see cref="DocumentIsCalculatedEvent"/>
        /// </summary>
        /// <param name="PagesCount"></param>
        public delegate void DocumentIsCalculated(int PagesCount);
        /// <summary>
        /// Happens when all varribles (such page H, document pages and etc.) are calculated and will be processed
        /// </summary>
        public event DocumentIsCalculated? DocumentIsCalculatedEvent;

        /// <summary>
        /// Represends Unified Corner Control
        /// </summary>
        public struct MarginPadding
        {
            public int Left, Top, Right, Bottom;

            public MarginPadding(int value)
            {
                Left =
                Top =
                Right =
                Bottom
                = value;
            }

            public MarginPadding(int RightLeft, int TopBottom)
            {
                Left = Right = RightLeft;
                Top = Bottom = TopBottom;
            }

            public MarginPadding(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }
        public struct ImageFormingProfile
        {
            public int KanjiBlockH;
            public int BlockSpacingH;

            public int KanjiSymbolFontSize;
            public int KunReadingFontSize;
            public int OnReadingFontSize;
            public int TranslationFontSize;

            public string JapaneseTextFontFamily, OtherTextFontFamily;

            public MarginPadding ContentPadding;

            public Size KanjiDescriptionWH, PaperPixelWH;

            public float KanjiPropotionW, PronunciationPropotionW, TranslationPropotionW, KeysPropotionW;

            public Rgba32 KanjiColor, Background, OnReadingColor, KunReadingColor;
        }

        /// <summary>
        /// Standart ISO 216 Page pixel WH
        /// </summary>
        public static readonly Size A4PaperPixelWH_ISO216 = new Size(1480, 3508);

        public static readonly ImageFormingProfile DefultProfile = new ImageFormingProfile()
        {
            KanjiBlockH = 400,
            BlockSpacingH = 20,
            PaperPixelWH = A4PaperPixelWH_ISO216,
            ContentPadding = new MarginPadding(10),

            KanjiSymbolFontSize = 40,
            KanjiColor = new Rgba32(0, 0, 0, 255),
            TranslationFontSize = 20,

            KanjiPropotionW = 20,
            KeysPropotionW = 20,
            TranslationPropotionW = 40,
            PronunciationPropotionW = 40,

            JapaneseTextFontFamily = "MS Mincho",
            OtherTextFontFamily = "Times New Roman"
        };


        /// <summary>
        /// Creates a list of Kanjies for print or saving
        /// </summary>
        /// <param name="KanjiTable">Kanji table for imaginig</param>
        /// <param name="profile">Rules of colours, poisitions. if defult then will be taken defult profile</param>
        /// <param name="start">Determines the first KanjiID in the given Kanji Table. -1 means all</param>
        /// <param name="end">Determines the end( in the range) Kanji in the given Kanji Table. -1 means all</param>
        /// <returns></returns>
        public List<Image<Rgba32>?>? CompileKanjiDictionary(List<Kanji> KanjiTable, ImageFormingProfile profile = default, int start = -1, int end = -1)
        {
            if (KanjiTable == null || KanjiTable.Count == 0)
                throw new NullReferenceException("KanjiTable is empty or null");

            if (profile.Equals(default(ImageFormingProfile)))
                profile = DefultProfile;


            int ImageW = 0,  BlockW = profile.PaperPixelWH.Width - (profile.ContentPadding.Left + profile.ContentPadding.Right);

            // TODO: Add support for font matching
            int KanjiBlockWbyProportion = (int)(BlockW * profile.KanjiPropotionW) / 100;
            int PronunciationBlockWbyPropotion = (int)(BlockW * profile.PronunciationPropotionW) / 100;
            int TranslationBlockWbyPropotion = (int)(BlockW * profile.TranslationPropotionW) / 100;
            int KeyBlockWbyPropotion = (int)(BlockW * profile.KeysPropotionW) / 100;
            int BlockWithSpacingH = profile.BlockSpacingH + profile.KanjiBlockH;
            // Pages forming

            int PerPageMaxBlocksCapacity = (int)Math.Round((double)profile.PaperPixelWH.Height / BlockWithSpacingH);
            int PagesDpendedCount = PerPageMaxBlocksCapacity / KanjiTable.Count;


            ImageW = profile.PaperPixelWH.Width;

            List<Image<Rgba32>>? ResultPages = new List<Image<Rgba32>>();


            this.DocumentIsCalculatedEvent?.Invoke(PagesDpendedCount);

            for (int pageI = 0, kanji_index = 0; pageI < PagesDpendedCount; pageI++)
            {

                Image<Rgba32> Page = new Image<Rgba32>(ImageW, profile.PaperPixelWH.Height);

                var JapaneseTextFont = SystemFonts.Get(profile.JapaneseTextFontFamily)
                    .CreateFont(profile.KanjiSymbolFontSize, SixLabors.Fonts.FontStyle.Regular);

                var TranslationTextFont = SystemFonts.Get(profile.OtherTextFontFamily)
                    .CreateFont(profile.TranslationFontSize, SixLabors.Fonts.FontStyle.Bold);

                for (int i = 0; i < profile.PaperPixelWH.Height; i++)
                {
                    for (int j = 0; j < ImageW; j++)
                    {
                        Page[j, i] = new Rgba32(255, 255, 255, 255);
                    }
                }

                for (int i = kanji_index, h = 0; i < KanjiTable.Count - kanji_index; i++, kanji_index++, h += profile.KanjiBlockH + profile.BlockSpacingH)
                {
                    Kanji CurrentKanji = KanjiTable[i];

                    Page.Mutate(mut => mut.DrawText(
                            KanjiTable[i].KanjiSymbol,
                            JapaneseTextFont,
                            profile.KanjiColor,
                            new PointF(profile.ContentPadding.Left, h)
                        ));

                    for (int j = 0; j < KanjiTable[i].Reading.Onyomi.Length; j++)
                    {


                        Page.Mutate(mut => mut.DrawText(
                                    KanjiTable[i].Reading.Onyomi[j],
                                    JapaneseTextFont,
                                    profile.KanjiColor,
                                    new PointF(
                                        profile.ContentPadding.Left + PronunciationBlockWbyPropotion
                                        , h + 20 * j)
                                ));
                    }
                    for (int j = 0; j < KanjiTable[i].Reading.Kunyomi.Length; j++)
                    {


                        Page.Mutate(mut => mut.DrawText(
                                    KanjiTable[i].Reading.Kunyomi[j],
                                    JapaneseTextFont,
                                    profile.KanjiColor,
                                    new PointF(
                                        profile.ContentPadding.Left + PronunciationBlockWbyPropotion
                                        + 150, h + 20 * j)
                                ));
                    }

                    for (int j = 0; j < KanjiTable[i].Meanings.Length; j++)
                    {


                        Page.Mutate(mut => mut.DrawText(
                                    KanjiTable[i].Meanings[j],
                                    TranslationTextFont,
                                    profile.KanjiColor,
                                    new PointF(
                                    profile.ContentPadding.Left + PronunciationBlockWbyPropotion + TranslationBlockWbyPropotion
                                    + 150, h + 20 * j)
                                ));
                    }
                    ResultPages.Add(Page);
                    this.PageReadyEvent?.Invoke(new PageEventInfo(pageI, new Size(Page.Width, Page.Height), Page));
                }

            }
            DocumentIsReadyEvent?.Invoke(ResultPages.Count);
            return ResultPages;
        }

    }
}

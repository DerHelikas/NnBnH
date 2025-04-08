using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.Models
{


    /// <summary>
    /// Declares struct that says how this Kanji should be written and spoken
    /// </summary>
    public struct KanjiReading
    {
        public KanjiReading(string[] onyomi, string[] kunyomi)
        {
            Onyomi = onyomi;
            Kunyomi = kunyomi;
        }

        /// <summary>
        /// On-readings
        /// </summary>
        public string[] Onyomi { get; private set; }
        
        /// <summary>
        /// Kun-readings
        /// </summary>
        public string[] Kunyomi { get; private set; }
    }

    /// <summary>
    /// Declares class that declares Kanji (not composed with other). 
    /// <para>Contains two readings <see cref="KanjiReading"/></para>
    /// </summary>
    public class Kanji
    {
        public Kanji(string kanjiSymbol, KanjiReading reading)
        {
            KanjiSymbol = kanjiSymbol;
            this.Reading = reading;
        }


        public KanjiReading Reading { get; internal set; } = new KanjiReading(new string[0], new string[0]);
        
        /// <summary>
        /// Element of a kanji. 私 for example
        /// </summary>
        public string KanjiSymbol { get; private set; } = string.Empty;
        
        /// <summary>
        /// English translation of this kanji
        /// </summary>
        public string[] Meanings { get; internal set; } = new string[0];

        /// <summary>
        /// Keys that used for compose this Kanji. 
        /// <para>本 for example</para>
        /// </summary>
        public string[] Keys { get; internal set; } = new string[0];


    }
}

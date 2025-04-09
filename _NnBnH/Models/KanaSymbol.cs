using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.Models
{

    /// <summary>
    /// Declares rowed and grouped KanaTable  [usually vars which use this class use [name]Table naming] 
    /// <code>
    ///     Example:
    ///         K* : ka ki ku ke ko
    ///         s**: sa shi su se so | sho shu shy
    /// </code>
    /// </summary>
    public class KanaGroupedLine
    {
        public KanaGroupedLine() { }

        public KanaGroupedLine(List<KanaSymbol> row)
        {
            Row = row;
        }

        public KanaGroupedLine(string rowName, List<KanaSymbol> row)
        {
            Row = row;
            RowName = rowName;
        }

        public List<KanaSymbol> Row { get; set; }
        public string RowName { get; set; }
    }

    /// <summary>
    /// <para>Describes how this Kana should be written in</para>
    /// <para>Hiragana and Katagana</para>
    /// </summary>
    public class KanaWritting
    {
        public KanaWritting(string hiragana, string katagana)
        {
            Hiragana = hiragana;
            Katagana = katagana;
        }

        public string Hiragana { get; set; }
        public string Katagana { get; set; }
    }

    /// <summary>
    /// A kana letter with pronunciation and writting 
    /// </summary>
    public class KanaSymbol
    {
        public KanaSymbol(KanaWritting kana, string prononseation)
        {
            Kana = kana;
            this.Romanji = prononseation;
        }

        public KanaWritting Kana { get; private set; } = new KanaWritting("","");
        public string Romanji { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.Models.TrainingElements
{
    public enum Type
    {
        /// <summary>
        /// Audio
        /// </summary>
        A,
        /// <summary>
        /// VideoAudio
        /// </summary>
        VA,
        /// <summary>
        /// TextOnly
        /// </summary>
        T
    }


    public class TrainingElement
    {
        public TrainingElement(Type type, string iD, string author)
        {
            this.type = type;
            ID = iD;
            Author = author;
        }

        public Type type { get; protected set; } = Type.T;

        public string ID { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;

    }
}

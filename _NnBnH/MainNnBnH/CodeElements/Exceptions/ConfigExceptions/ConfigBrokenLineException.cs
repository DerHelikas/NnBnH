using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.CodeElements.Exceptions.ConfigExceptions
{
    internal class ConfigBrokenLineException : Exception
    {
        public ConfigBrokenLineException(int line)
        : base($"{line} is not valid")
        {
        }

      
    }
}

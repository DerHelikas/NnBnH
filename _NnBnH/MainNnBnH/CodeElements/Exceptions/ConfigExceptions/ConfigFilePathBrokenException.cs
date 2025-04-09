using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.CodeElements.Exceptions.ConfigExceptions
{
    internal class ConfigFilePathBrokenException : Exception
    {
        public ConfigFilePathBrokenException()
        {
        }

        public ConfigFilePathBrokenException(string? message) : base(message)
        {
        }

        public ConfigFilePathBrokenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}

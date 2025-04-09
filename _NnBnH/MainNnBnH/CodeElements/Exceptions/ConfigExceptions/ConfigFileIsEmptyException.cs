using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.CodeElements.Exceptions.ConfigExceptions
{
    public class ConfigFileIsEmptyException : Exception
    {
        public ConfigFileIsEmptyException()
        {
        }

        public ConfigFileIsEmptyException(string? message) : base(message)
        {
        }

        public ConfigFileIsEmptyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}

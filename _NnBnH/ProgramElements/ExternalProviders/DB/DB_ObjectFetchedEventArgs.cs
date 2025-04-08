using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.ExternalProviders.DB
{
    /// <summary>
    /// A common class for each DB data manipulation Event Args
    /// </summary>
    public class DB_ObjectFetchedEventArgs : EventArgs
    {
        public Type? OjectType { get; private set; }
        public object? Return { get; private set; }
        public Exception? exception { get; private set; }
        public DateTime DateTime { get; private set; }
        public bool failed { get; internal set; }


        public DB_ObjectFetchedEventArgs(Type? ojectType, object? @return, Exception? exception, DateTime dateTime)
        {
            OjectType = ojectType;
            Return = @return;
            this.exception = exception;
            DateTime = dateTime;
        }
    }
}

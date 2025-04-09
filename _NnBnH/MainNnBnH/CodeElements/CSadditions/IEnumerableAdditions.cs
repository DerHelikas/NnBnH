using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.CodeElements.CSadditions
{
    public static class IEnumerableAdditions
    {

        /// <summary>
        /// Extends IEnumerable.
        /// 
        /// Checks if index is in range of elements count in 
        /// the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsInRange<T>(this IEnumerable<T> enumerable, int index)
        {
            
            return !(index < 0) && !(index > enumerable.Count());
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumer) => enumer?.Count() == 0 || enumer == null;
    }
}

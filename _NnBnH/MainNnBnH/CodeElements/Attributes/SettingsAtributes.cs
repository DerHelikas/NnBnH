using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.CodeElements.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field)]
    class NonSerialisableSettingFieldAttribute : Attribute
    {
        public NonSerialisableSettingFieldAttribute()
        {

        }
    }
}

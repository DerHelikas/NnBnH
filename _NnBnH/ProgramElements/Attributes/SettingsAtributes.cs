using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.Attributes.SettingsAtributes
{

    [System.AttributeUsage(System.AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field)]
    class NonSerialisableSettingFieldAttribute : System.Attribute
    {
        public NonSerialisableSettingFieldAttribute()
        {

        }
    }
}

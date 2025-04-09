using _NnBnH.Models;
using _NnBnH.MainNnBnH.CodeElements.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _NnBnH.MainNnBnH.RuntimeElements
{
    /// <summary>
    /// Variables which lifetime takes within program's runtime. (DRV During Runtime Variables)
    /// <para>Program start---------- Enviroument.exit([code])</para>
    /// </summary>
    public static class DuringRuntimeVariables
    {

        /// <summary>
        /// Current in Memory KanaTable
        /// </summary>
        public static List<KanaGroupedLine> ActuallKanaTable { get; private set; } = new List<KanaGroupedLine>();

        /// <summary>
        /// Sets if program has turned into an Autonomus mode (withotu internet, web API access)
        /// </summary>
        private static bool _isAutonomusMode { get; set; } = true;

        public static bool IsAutonomusMode
        {
            get => _isAutonomusMode; set
            {
                _isAutonomusMode = value;
            }
        }


        /// <summary>
        /// Substitutes the DRV's kana table
        /// </summary>
        /// <param name="KanaTable">Substitution for the DRV's kana table</param>
        /// <exception cref="ParameterNullException">Throws when KanaTable is null</exception>
        public static void ChangeKanaTable(List<KanaGroupedLine> KanaTable)
        {

            ActuallKanaTable = KanaTable ??
                throw new ArgumentNullException($"KanaTable is null. Variables have not been changed");
        }
    }
}

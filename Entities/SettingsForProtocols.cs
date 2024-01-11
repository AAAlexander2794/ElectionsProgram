using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Настройки для протоколов жеребьевки.
    /// </summary>
    public class SettingsForProtocols
    {
        public SettingsForProtocolsView View { get; set; }

        public SettingsForProtocols(SettingsForProtocolsView view) 
        {
            View = view;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Запись вещания по факту выхода в эфир.
    /// </summary>
    public class BroadcastActual
    {
        public BroadcastActualView View { get; set; }

        #region Конструкторы

        public BroadcastActual(BroadcastActualView view)
        {
            View = view;
        }

        public BroadcastActual() { }

        #endregion Конструкторы
    }
}

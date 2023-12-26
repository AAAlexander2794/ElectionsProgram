using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Запись о вещании по плану.
    /// </summary>
    /// <remarks>
    /// Из таких записей состоит <see cref="Talon"/>.
    /// </remarks>
    public class BroadcastNominal
    {
        public BroadcastNominalView View { get; set; }

        #region Конструкторы

        public BroadcastNominal(BroadcastNominalView view)
        {
            View = view;
        }

        public BroadcastNominal() { }

        #endregion Конструкторы
    }
}

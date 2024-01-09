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
    public class TalonRecord
    {
        public TalonRecordView View { get; set; }

        #region Конструкторы

        public TalonRecord(TalonRecordView view)
        {
            View = view;
        }

        public TalonRecord() { }

        #endregion Конструкторы
    }
}

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
    public class PlaylistRecord
    {
        public PlaylistRecordView View { get; set; }

        #region Конструкторы

        public PlaylistRecord(PlaylistRecordView view)
        {
            View = view;
        }

        public PlaylistRecord() { }

        #endregion Конструкторы
    }
}

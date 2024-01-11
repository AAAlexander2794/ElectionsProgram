using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public class Talon
    {
        public int Id { get; set; }

        /// <summary>
        /// Медиаресурс, к которому относится талон
        /// </summary>
        public Mediaresource? Mediaresource { get; set; }

        /// <summary>
        /// Название медиаресурса
        /// </summary>
        public string MediaresourceName { get; set; }

        /// <summary>
        /// Номер талона
        /// </summary>
        public string Number {  get; set; }

        /// <summary>
        /// Записи вещания номинальные (при составлении талонов)
        /// </summary>
        public ObservableCollection<TalonRecord> BroadcastsNominal { get; set; } = new ObservableCollection<TalonRecord>();

        /// <summary>
        /// Строки вещания, общие для группы клиентов.
        /// </summary>
        public ObservableCollection<TalonRecord> BroadcastsCommon { get; set; } = new ObservableCollection<TalonRecord>();

        ///// <summary>
        ///// Записи вещания фактические (по вещанию)
        ///// </summary>
        //public ObservableCollection<PlaylistRecord> BroadcastsActual { get; set; } = new ObservableCollection<PlaylistRecord>();

        public Talon(string mediaresourceName, string number)
        {
            MediaresourceName = mediaresourceName;
            Number = number;
        }
    }
}

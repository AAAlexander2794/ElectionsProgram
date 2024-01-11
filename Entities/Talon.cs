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
        public ObservableCollection<TalonRecord> TalonRecords { get; set; } = new ObservableCollection<TalonRecord>();

        /// <summary>
        /// Строки вещания, общие для группы клиентов.
        /// </summary>
        public ObservableCollection<TalonRecord> CommonRecords { get; set; } = new ObservableCollection<TalonRecord>();

        public Talon(string mediaresourceName, string number)
        {
            MediaresourceName = mediaresourceName;
            Number = number;
        }

        /// <summary>
        /// Возвращает хронометраж суммарный по всем записям талона (без общего вещания).
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetDurationTalonRecords()
        {
            TimeSpan duration = TimeSpan.Zero;
            foreach (var record in TalonRecords)
            {
                duration += record.DurationNominal;
            }
            return duration;
        }

        /// <summary>
        /// Возвращает хронометраж суммарный по всем записям общего вещания (без записей талона).
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetDurationCommonRecords()
        {
            TimeSpan duration = TimeSpan.Zero;
            foreach (var record in CommonRecords)
            {
                duration += record.DurationNominal;
            }
            return duration;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public class Talon
    {
        //public int Id { get; set; }

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

        ///// <summary>
        ///// Строки вещания, общие для группы клиентов.
        ///// </summary>
        //public ObservableCollection<TalonRecord> CommonRecords { get; set; } = new ObservableCollection<TalonRecord>();

        public Talon? CommonTalon { get; set; }

        public Talon(string mediaresourceName, string number)
        {
            MediaresourceName = mediaresourceName;
            Number = number;
        }

        /// <summary>
        /// Возвращает хронометраж суммарный по всем записям талона (без общего вещания).
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetDurationTalonRecords(string mode = "digit")
        {
            TimeSpan duration = TimeSpan.Zero;
            if (mode == "digit")
            {
                foreach (var record in TalonRecords)
                {
                    duration += record.Duration;
                }
            }
            if (mode == "text")
            {
                foreach (var record in TalonRecords)
                {
                    duration += TimeSpan.FromSeconds(double.Parse(record.View.Duration));
                }
            }
            return duration;
        }

        /// <summary>
        /// Возвращает хронометраж суммарный по всем записям общего вещания (без записей талона).
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetDurationCommonRecords(string mode = "digit")
        {
            TimeSpan duration = TimeSpan.Zero;
            if (CommonTalon == null) { return TimeSpan.Zero; }
            if (mode == "digit")
            {
                foreach (var record in CommonTalon.TalonRecords)
                {
                    duration += record.Duration;
                }
            }
            if (mode == "text")
            {
                foreach (var record in CommonTalon.TalonRecords)
                {
                    double d = double.Parse(record.View.Duration);
                    var time = TimeSpan.FromSeconds(d);
                    duration += time;
                }

            }
            return duration;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

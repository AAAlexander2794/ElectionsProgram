using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Processors
{
    public static class Logger
    {
        public static ObservableCollection<string> LogRecords { get; } = new ObservableCollection<string>();

        public static void Add(string message)
        {
            var record = $"{DateTime.Now} {message}";
            // Добавляем в начало
            LogRecords.Insert(0, record);
        }

        
    }
}

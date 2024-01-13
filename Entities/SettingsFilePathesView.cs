using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Настройки путей к файлам
    /// </summary>
    public class SettingsFilePathesView
    {
        #region Протоколы

        /// <summary>
        /// Путь к папке протоколов жеребьевки.
        /// </summary>
        public string Протоколы_Выходной_каталог { get; set; } = @"Документы\Протоколы\";

        public string Протоколы_Шаблон_Партии { get; set; } = @"Настройки\Протоколы\Приложение 1.dotx";

        public string Протоколы_Настройки { get; set; } = @"Настройки\Протоколы\Протоколы.xlsx";

        #endregion Протоколы

        #region Партии

        public string Партии_Таблица { get; set; } = @"Настройки\Партии\Партии.xlsx";

        // Талоны бесплатные

        public string Партии_Талоны_Россия_1 { get; set; }

        public string Партии_Талоны_Россия_24 { get; set; }

        public string Партии_Талоны_Маяк { get; set; }

        public string Партии_Талоны_Вести_ФМ { get; set; }

        public string Партии_Талоны_Радио_России { get; set; }

        // Время общего вещания

        public string Партии_Общее_Россия_1 { get; set; }

        public string Партии_Общее_Россия_24 { get; set; }

        public string Партии_Общее_Маяк { get; set; }

        public string Партии_Общее_Вести_ФМ { get; set; }

        public string Партии_Общее_Радио_России { get; set; }

        // Шаблоны

        public string Партии_Шаблон_договора_РВ { get; set; }

        public string Партии_Шаблон_договора_ТВ { get; set; }

        #endregion Партии
    }
}

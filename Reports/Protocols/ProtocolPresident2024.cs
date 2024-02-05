using DocumentFormat.OpenXml.Wordprocessing;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectionsProgram.Reports.Protocols
{
    /// <summary>
    /// Строит протокол жеребьевки предвыборной кампании на выборы Президента 2024 года.
    /// </summary>
    /// <remarks>
    /// Выборы 2024.03.17.
    /// </remarks>
    public static class ProtocolPresident2024
    {

        /// <summary>
        /// Строит совместный протокол на кандидатов и партии по каждому СМИ.
        /// </summary>
        /// <remarks>
        /// Партии должны содержать кандидата, которого выдвигают. Кандидаты отсортированы в 
        /// алфавитном порядке.
        /// </remarks>
        public static void BuildProtocols(
            List<Candidate> candidates,
            List<Party> parties,
            SettingsForProtocols settings,
            string protocolFolderPath,
            string templatePath)
        {
            // Сортируем в алфавитном порядке
            candidates.OrderBy(c => c.View.Фамилия);
            parties.OrderBy(p => p.View.Кандидат_Фамилия_Падеж_им);
            // Дата и время для формирования одной папки протоколов
            var subfolder = DateTime.Now.ToString().Replace(":", "_");
            // Формируем путь к документу (с промежуточной папкой текущего времени)
            var resultPath = $"{protocolFolderPath}{subfolder}\\";
            // Надо очистить путь от знаков, которыми нельзя называть каталоги
            resultPath = Regex.Replace(resultPath, "\"", "");
            // Создает путь для документов, если вдруг каких-то папок нет
            Directory.CreateDirectory(resultPath);
            // По каждому СМИ
            CreateProtocol(candidates, parties, settings, templatePath, resultPath, "Маяк");
            CreateProtocol(candidates, parties, settings, templatePath, resultPath, "Вести ФМ");
            CreateProtocol(candidates, parties, settings, templatePath, resultPath, "Радио России");
            CreateProtocol(candidates, parties, settings, templatePath, resultPath, "Россия 1");
            CreateProtocol(candidates, parties, settings, templatePath, resultPath, "Россия 24");
        }



        /// <summary>
        /// Формирует файл протокола кандидатов округа
        /// </summary>
        private static void CreateProtocol(List<Candidate> candidates, List<Party> parties, SettingsForProtocols settings, string templatePath, string resultPath, string mediaresource)
        {
            //
            string fieldMedia = "";
            string fileName = "_";
            switch (mediaresource)
            {
                case "Маяк":
                    fieldMedia = settings.View.Название_СМИ_Маяк;
                    fileName = "Маяк.docx";
                    break;
                case "Вести ФМ":
                    fieldMedia = settings.View.Название_СМИ_Вести_ФМ;
                    fileName = "Вести ФМ.docx";
                    break;
                case "Радио России":
                    fieldMedia = settings.View.Название_СМИ_Радио_России;
                    fileName = "Радио России.docx";
                    break;
                case "Россия 1":
                    fieldMedia = settings.View.Название_СМИ_Россия_1;
                    fileName = "Россия 1.docx";
                    break;
                case "Россия 24":
                    fieldMedia = settings.View.Название_СМИ_Россия_24;
                    fileName = "Россия 24.docx";
                    break;
            }
            // Новый документ протокола
            var document = new WordDocument(templatePath);
            // Заполняем поля слияния
            document.SetMergeFieldText("Наименование_СМИ", $"{fieldMedia}");
            document.SetMergeFieldText("ИО_Фамилия_предст_СМИ", $"{settings.View.Кандидаты_Предст_СМИ_ИО_Фамилия}");
            document.SetMergeFieldText("Дата", $"{settings.View.Кандидаты_Дата}");
            document.SetMergeFieldText("ИО_Фамилия_члена_изб_ком", $"{settings.View.Кандидаты_Член_изб_ком_ИО_Фамилия}");
            // Вставляем таблицу c талонами кандидатов
            document.SetBookmarkText($"Таблица_кандидатов", "");
            var dtCandidates = CreateDataTableCandidates(candidates, mediaresource, settings);
            var tableCandidates = WordDocument.CreateTable(dtCandidates, "20");
            document.SetBookmarkTable($"Таблица_кандидатов", tableCandidates);
            // Вставляем таблицу с талонами партий
            document.SetBookmarkText($"Таблица_партий", "");
            var dtParties = CreateDataTableParties(parties, mediaresource, settings);
            var tableParties = WordDocument.CreateTable(dtParties, "20");
            document.SetBookmarkTable($"Таблица_партий", tableParties);
            // Сохраняем и закрываем документ
            document.Save(resultPath + $"{fileName}");
            document.Close();
        }

        /// <summary>
        /// Захардкоженная таблица кандидатов
        /// </summary>
        /// <param name="talon"></param>
        /// <returns></returns>
        static DataTable CreateDataTableCandidates(List<Candidate> candidates, string mediaresource, SettingsForProtocols settings)
        {
            //
            DataTable dt = new DataTable();
            // Заголовки таблицы
            // 1
            dt.Columns.Add($"№ п/п");
            // 2
            dt.Columns.Add($"Фамилия, имя, отчество\r\n" +
                "зарегистрированного кандидата\r\n" +
                "(фамилии указываются\r\n" +
                "в алфавитном порядке)\r\n");
            // 3
            dt.Columns.Add($"Даты и время выхода в эфир совместных агитационных мероприятий");
            // 4
            dt.Columns.Add($"Даты и время выхода в эфир иных агитационных материалов");
            // 5
            dt.Columns.Add($"Фамилия, инициалы зарегистрированного кандидата (его представителя), " +
                $"участвовавшего в жеребьевке (члена соответствующей " +
                $"избирательной комиссии с правом решающего голоса)");
            // 6
            dt.Columns.Add($"Подпись зарегистрированного кандидата (его представителя), " +
                $"участвовавшего в жеребьевке (члена соответствующей избирательной " +
                $"комиссии с правом решающего голоса), и дата подписания");
            
            //// Добавляем строчку с нумерованием столбцов
            //dt.Rows.Add();
            //dt.Rows[dt.Rows.Count - 1][0] = "1";
            //dt.Rows[dt.Rows.Count - 1][1] = "2";
            //dt.Rows[dt.Rows.Count - 1][2] = "3";
            //dt.Rows[dt.Rows.Count - 1][3] = "4";
            //dt.Rows[dt.Rows.Count - 1][4] = "5";
            //dt.Rows[dt.Rows.Count - 1][5] = "6";

            // Для строки Итого со всех кандидатов длительность берем (талонов и общего вещания)
            TimeSpan duration = TimeSpan.Zero;
            TimeSpan durationCommonRecords = TimeSpan.Zero;
            // Счетчик кандидатов
            int count = 0;
            // По каждому кандидату
            foreach (var candidate in candidates)
            {
                //
                count++;
                //
                if (candidate == null ||
                    candidate.View == null ||
                    candidate.View.На_печать == "") continue;
                // Добавляем пустую строку
                dt.Rows.Add();
                //
                string cell5Text = "";
                //
                if (candidate.View.Явка_кандидата == "1")
                {
                    // Если кандидат внесен (вообще и так должен быть внесен)
                    if (candidate.View.Фамилия.Length > 0 &&
                    candidate.View.Имя.Length > 0 &&
                    candidate.View.Отчество.Length > 0)
                    {
                        // Вносим ФИО кандидата
                        cell5Text = 
                            $"{candidate.View.Фамилия} " +
                            $"{candidate.View.Имя[0]}. " +
                            $"{candidate.View.Отчество[0]}.";
                    }
                }
                else if (candidate.View.Явка_представителя == "1")
                {
                    // Если Представитель внесен
                    if (candidate.View.Представитель_Фамилия.Length > 0 &&
                    candidate.View.Представитель_Имя.Length > 0 &&
                    candidate.View.Представитель_Отчество.Length > 0)
                    {
                        // Вносим ФИО представителя
                        cell5Text =
                            $"{candidate.View.Представитель_Фамилия} " +
                            $"{candidate.View.Представитель_Имя[0]}. " +
                            $"{candidate.View.Представитель_Отчество[0]}.";
                    }
                }
                else
                {
                    // Вносим ФИО члена Избиркома
                    cell5Text = $"{settings.View.Кандидаты_Член_изб_ком_Фамилия_ИО}";
                }
                //
                Talon? talon = null;
                // Определяем, какой из талонов надо использовать
                switch (mediaresource)
                {
                    case "Маяк":
                        talon = candidate.Талон_Маяк;
                        break;
                    case "Вести ФМ":
                        talon = candidate.Талон_Вести_ФМ;
                        break;
                    case "Радио России":
                        talon = candidate.Талон_Радио_России;
                        break;
                    case "Россия 1":
                        talon = candidate.Талон_Россия_1;
                        break;
                    case "Россия 24":
                        talon = candidate.Талон_Россия_24;
                        break;
                }
                // Для общей длительности в Итого
                if (talon != null)
                {
                    duration += talon.GetDurationTalonRecords();
                    durationCommonRecords += talon.GetDurationCommonRecords();
                }
                // Если у кандидата есть фамилия и этот талон (запасная проверка)
                if (candidate.View.Фамилия != "" && talon != null)
                {
                    // Делаем строку кандидата
                    FillRowCandidate(dt.Rows[dt.Rows.Count - 1], candidate, talon, mediaresource, count, cell5Text, settings.View.Кандидаты_Дата);
                }             
            }
            // Строка "Итого"
            dt.Rows.Add();
            //
            dt.Rows[dt.Rows.Count - 1][0] = $"Итого";
            dt.Rows[dt.Rows.Count - 1][1] = $"";
            // Длительность совместных агитационных материалов
            if (durationCommonRecords != TimeSpan.Zero)
            {
                dt.Rows[dt.Rows.Count - 1][2] = $"{durationCommonRecords}";
            }
            else
            {
                dt.Rows[dt.Rows.Count - 1][2] = $"";
            }
            // Длительность иных агитационных материалов
            if (duration != TimeSpan.Zero)
            {
                dt.Rows[dt.Rows.Count - 1][3] = $"{duration}";
            }
            else
            {
                dt.Rows[dt.Rows.Count - 1][3] = $"";
            }
            //
            dt.Rows[dt.Rows.Count - 1][4] = $"";
            dt.Rows[dt.Rows.Count - 1][5] = $"";
            //
            return dt; 
        }

        private static void FillRowCandidate(
            DataRow dataRow,
            Candidate candidate,
            Talon talon,
            string mediaresource,
            int i,
            string cell5Text,
            string cell6Text)
        {
            // Добавляем номер талона в текст для ячейки талона
            string talonText = $"Талон № {talon.Number}";
            //
            foreach (var row in talon.TalonRecords)
            {
                talonText += $"\r\n{row.Date} {row.Time} {row.Duration} {row.Description}";
            }
            // Текст ячейки с общим вещанием
            string commonTalonText = "";
            //
            if (talon.CommonTalon != null)
            {
                //
                foreach (var row in talon.CommonTalon.TalonRecords)
                {
                    commonTalonText += $"\r\n{row.Date} {row.Time} {row.Duration} {row.Description}";
                }
            }
            // 
            dataRow[0] = $"{i}";
            dataRow[1] = $"{candidate.View.Фамилия} {candidate.View.Имя} {candidate.View.Отчество}";
            dataRow[2] = $"{commonTalonText}";
            dataRow[3] = $"{talonText}";
            dataRow[4] = $"{cell5Text}";
            dataRow[5] = $"{cell6Text}";
            //
            return;
        }

        static DataTable CreateDataTableParties(List<Party> parties, string mediaresource, SettingsForProtocols settings)
        {
            //
            DataTable dt = new DataTable();
            // Заголовки таблицы
            // 1
            dt.Columns.Add($"№ п/п");
            // 2
            dt.Columns.Add($"Наименование политической партии, выдвинувшей " +
                $"зарегистрированного кандидата (наименования располагаются в " +
                $"алфавитном порядке фамилий выдвинутых политическими партиями кандидатов)");
            // 3
            dt.Columns.Add($"Даты и время выхода в эфир совместных агитационных мероприятий");
            // 4
            dt.Columns.Add($"Даты и время выхода в эфир иных агитационных материалов");
            // 5
            dt.Columns.Add($"Фамилия, инициалы представителя политической партии, " +
                $"участвовавшего в жеребьевке (члена соответствующей " +
                $"избирательной комиссии с правом решающего голоса)");
            // 6
            dt.Columns.Add($"Подпись представителя политической партии, " +
                $"участвовавшего в жеребьевке (члена соответствующей " +
                $"избирательной комиссии с правом решающего голоса), и дата подписания");
            
            //// Добавляем строчку с нумерованием столбцов
            //dt.Rows.Add();
            //dt.Rows[dt.Rows.Count - 1][0] = "1";
            //dt.Rows[dt.Rows.Count - 1][1] = "2";
            //dt.Rows[dt.Rows.Count - 1][2] = "3";
            //dt.Rows[dt.Rows.Count - 1][3] = "4";
            //dt.Rows[dt.Rows.Count - 1][4] = "5";
            //dt.Rows[dt.Rows.Count - 1][5] = "6";

            // Для строки Итого со всех кандидатов длительность берем (талонов и общего вещания)
            TimeSpan duration = TimeSpan.Zero;
            TimeSpan durationCommonRecords = TimeSpan.Zero;
            // Счетчик кандидатов
            int count = 0;
            // По каждому кандидату
            foreach (var party in parties)
            {
                //
                count++;
                //
                if (party == null ||
                    party.View == null ||
                    party.View.На_печать == "") continue;
                // Добавляем пустую строку
                dt.Rows.Add();
                //
                string cell5Text = "";
                //
                if (party.View.Явка == "1")
                {
                    // Если Представитель внесен
                    if (party.View.Представитель_Фамилия_Падеж_им.Length > 0 &&
                    party.View.Представитель_Имя_Падеж_им.Length > 0 &&
                    party.View.Представитель_Отчество_Падеж_им.Length > 0)
                    {
                        // Вносим ФИО представителя
                        cell5Text =
                            $"{party.View.Представитель_Фамилия_Падеж_им} " +
                            $"{party.View.Представитель_Имя_Падеж_им[0]}. " +
                            $"{party.View.Представитель_Отчество_Падеж_им[0]}.";
                    }
                }
                else
                {
                    // Вносим ФИО члена Избиркома
                    cell5Text = $"{settings.View.Кандидаты_Член_изб_ком_Фамилия_ИО}";
                }
                //
                Talon? talon = null;
                // Определяем, какой из талонов надо использовать
                switch (mediaresource)
                {
                    case "Маяк":
                        talon = party.Талон_Маяк;
                        break;
                    case "Вести ФМ":
                        talon = party.Талон_Вести_ФМ;
                        break;
                    case "Радио России":
                        talon = party.Талон_Радио_России;
                        break;
                    case "Россия 1":
                        talon = party.Талон_Россия_1;
                        break;
                    case "Россия 24":
                        talon = party.Талон_Россия_24;
                        break;
                }
                // Для общей длительности в Итого
                if (talon != null)
                {
                    duration += talon.GetDurationTalonRecords();
                    durationCommonRecords += talon.GetDurationCommonRecords();
                }
                // Если у кандидата есть фамилия и этот талон (запасная проверка)
                if (party.View.Кандидат_Фамилия_Падеж_им != "" && talon != null)
                {
                    // Делаем строку кандидата
                    FillRowParty(dt.Rows[dt.Rows.Count - 1], party, talon, mediaresource, count, cell5Text, settings.View.Кандидаты_Дата);
                }
            }
            // Строка "Итого"
            dt.Rows.Add();
            //
            dt.Rows[dt.Rows.Count - 1][0] = $"Итого";
            dt.Rows[dt.Rows.Count - 1][1] = $"";
            // Длительность совместных агитационных материалов
            if (durationCommonRecords != TimeSpan.Zero)
            {
                dt.Rows[dt.Rows.Count - 1][2] = $"{durationCommonRecords}";
            }
            else
            {
                dt.Rows[dt.Rows.Count - 1][2] = $"";
            }
            // Длительность иных агитационных материалов
            if (duration != TimeSpan.Zero)
            {
                dt.Rows[dt.Rows.Count - 1][3] = $"{duration}";
            }
            else
            {
                dt.Rows[dt.Rows.Count - 1][3] = $"";
            }
            //
            dt.Rows[dt.Rows.Count - 1][4] = $"";
            dt.Rows[dt.Rows.Count - 1][5] = $"";
            //
            return dt;
        }

        private static void FillRowParty(
            DataRow dataRow,
            Party party,
            Talon talon,
            string mediaresource,
            int i,
            string cell5Text,
            string cell6Text)
        {
            // Добавляем номер талона в текст для ячейки талона
            string talonText = $"Талон № {talon.Number}";
            //
            foreach (var row in talon.TalonRecords)
            {
                talonText += $"\r\n{row.Date} {row.Time} {row.Duration} {row.Description}";
            }
            // Текст ячейки с общим вещанием
            string commonTalonText = "";
            //
            if (talon.CommonTalon != null)
            {
                //
                foreach (var row in talon.CommonTalon.TalonRecords)
                {
                    commonTalonText += $"\r\n{row.Date} {row.Time} {row.Duration} {row.Description}";
                }
            }
            // 
            dataRow[0] = $"{i}";
            dataRow[1] = $"{party.View.Кандидат_Фамилия_Падеж_им} {party.View.Представитель_Имя_Падеж_им} {party.View.Представитель_Отчество_Падеж_им}";
            dataRow[2] = $"{commonTalonText}";
            dataRow[3] = $"{talonText}";
            dataRow[4] = $"{cell5Text}";
            dataRow[5] = $"{cell6Text}";
            //
            return;
        }

    }
}

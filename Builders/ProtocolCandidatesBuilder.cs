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

namespace ElectionsProgram.Builders
{

    public static class ProtocolCandidatesBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// По каждому СМИ по каждому округу делаем отдельный протокол.
        /// Разделяем по подпапкам округов, в каждой по 5 протоколов.
        /// </remarks>
        /// <param name="talonVariant"></param>
        /// <returns></returns>
        public static void BuildProtocolsCandidates(
            List<Region> regions,
            SettingsForProtocols settings,
            string protocolFolderPath,
            string templatePath)
        {
            // Дата и время для формирования одной папки под все округи протоколов
            var subfolder = DateTime.Now.ToString().Replace(":", "_");
            // По каждому округу делаем свои 5 протоколов на каждое СМИ
            foreach (var region in regions)
            {
                // Формируем путь к документу (с промежуточной папкой текущего времени)
                var resultPath = $"{protocolFolderPath}{subfolder}\\" +
                    $"{region.Номер.Trim()} {region.Название_Падеж_им.Trim()}\\";
                // Надо очистить путь от знаков, которыми нельзя называть каталоги
                resultPath = Regex.Replace(resultPath, "\"", "");
                // Создает путь для документов, если вдруг каких-то папок нет
                Directory.CreateDirectory(resultPath);
                // По каждому СМИ
                CreateProtocol(region, settings, templatePath, resultPath, "Маяк");
                CreateProtocol(region, settings, templatePath, resultPath, "Вести ФМ");
                CreateProtocol(region, settings, templatePath, resultPath, "Радио России");
                CreateProtocol(region, settings, templatePath, resultPath, "Россия 1");
                CreateProtocol(region, settings, templatePath, resultPath, "Россия 24");
            }
        }



        /// <summary>
        /// Формирует файл протокола кандидатов округа
        /// </summary>
        private static void CreateProtocol(Region region, SettingsForProtocols settings, string templatePath, string resultPath, string mediaresource)
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
            // Вставляем таблицу c талонами кандидатов округа
            document.SetBookmarkText($"Талон", "");
            var table = CreateTableProtocolCandidates(region, mediaresource, settings);
            document.SetBookmarkTable($"Талон", table);
            // Сохраняем и закрываем документ
            document.Save(resultPath + $"{fileName}");
            document.Close();
        }

        /// <summary>
        /// Захардкоженная таблица протокола кандидатов
        /// </summary>
        /// <param name="talon"></param>
        /// <returns></returns>
        static Table CreateTableProtocolCandidates(Region region, string mediaresource, SettingsForProtocols settings)
        {
            // 
            Table table = new Table();
            //
            TableProperties tblProp = new TableProperties();
            TableBorders tblBorders = new TableBorders()
            {
                BottomBorder = new BottomBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                TopBorder = new TopBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                LeftBorder = new LeftBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                RightBorder = new RightBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                InsideHorizontalBorder = new InsideHorizontalBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                InsideVerticalBorder = new InsideVerticalBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                }
            };
            tblProp.Append(tblBorders);
            //
            table.Append(tblProp);
            // Заголовки таблицы
            TableRow trHead = new TableRow();
            var tcH4 = new TableCell(WordDocument.CreateParagraph($"Даты и время\r\n" +
                $"выхода в эфир предвыборных\r\n" +
                $"агитационных материалов\r\n" +
                $"(число, месяц, год; время;\r\n" +
                $"количество\r\n" +
                $"минут/секунд"));
            tcH4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
            trHead.Append(
                new TableCell(WordDocument.CreateParagraph($"№ п/п")),
                new TableCell(WordDocument.CreateParagraph($"Фамилия, инициалы\r\n" +
                $"зарегистрированного кандидата,\r\n" +
                $"№ одномандатного\r\n" +
                $"избирательного округа, по\r\n" +
                $"которому он зарегистрирован")),
                new TableCell(WordDocument.CreateParagraph($"Даты и время выхода в эфир\r\n" +
                $"совместных агитационных\r\n" +
                $"мероприятий\r\n" +
                $"(число, месяц, год; время;\r\n" +
                $"количество\r\n" +
                $"минут/секунд")),
                tcH4,
                new TableCell(WordDocument.CreateParagraph($"Фамилия, инициалы представителя\r\n" +
                $"зарегистрированного кандидата,\r\n" +
                $"участвовавшего\r\n" +
                $"в жеребьевке (члена\r\n" +
                $"соответствующей\r\n" +
                $"избирательной комиссии с\r\n" +
                $"правом решающего голоса)")),
                new TableCell(WordDocument.CreateParagraph($"Подпись зарегистрированного кандидата,\r\n" +
                $"участвовавшего в жеребьевке\r\n" +
                $"(члена соответствующей\r\n" +
                $"избирательной комиссии с\r\n" +
                $"правом решающего голоса)\r\n" +
                $"и дата подписания"))
                );
            // Добавляем заголовок к таблице
            table.Append(trHead);
            // Добавляем строчку с нумерованием столбцов
            TableRow tr = new TableRow();
            TableCell tc1 = new TableCell(WordDocument.CreateParagraph($"1"));
            TableCell tc2 = new TableCell(WordDocument.CreateParagraph($"2"));
            TableCell tc3 = new TableCell(WordDocument.CreateParagraph($"3"));
            TableCell tc4 = new TableCell(WordDocument.CreateParagraph($"4"));
            TableCell tc5 = new TableCell(WordDocument.CreateParagraph($"5"));
            TableCell tc6 = new TableCell(WordDocument.CreateParagraph($"6"));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
            table.Append(tr);
            // Если у округа есть номер (если пустой, не указываем округ)
            if (region.Номер != "")
            {
                // Добавляем округ одной строкой
                tr = WordDocument.CreateRowMergedCells(region.Номер, 6);
                table.Append(tr);
            }
            // Для строки Итого со всех кандидатов длительность берем (талонов и общего вещания)
            TimeSpan duration = TimeSpan.Zero;
            TimeSpan durationCommonRecords =TimeSpan.Zero;
            // По каждому кандидату из протокола
            for (int i = 0; i < region.Candidates.Count; i++)
            {
                //
                var c = region.Candidates[i];
                // Ячейка с ФИО того, кто тянул талон и подписывает протокол
                string cell5Text = "";
                if (c.View.Явка_кандидата == "1")
                {
                    // Если кандидат внесен (вообще и так должен быть внесен)
                    if (c.View.Фамилия.Length > 0 &&
                    c.View.Имя.Length > 0 &&
                    c.View.Отчество.Length > 0)
                    {
                        cell5Text = $"{c.View.Фамилия} {c.View.Имя[0]}. {c.View.Отчество[0]}.";
                    }
                }
                else if (c.View.Явка_представителя == "1")
                {
                    // Если Представитель внесен
                    if (c.View.Представитель_Фамилия.Length > 0 &&
                    c.View.Представитель_Имя.Length > 0 &&
                    c.View.Представитель_Отчество.Length > 0)
                    {
                        cell5Text = $"{c.View.Представитель_Фамилия} {c.View.Представитель_Имя[0]}. {c.View.Представитель_Отчество[0]}.";
                    }
                }
                else
                {
                    cell5Text = $"{settings.View.Кандидаты_Член_изб_ком_Фамилия_ИО}";
                }
                //
                Talon? talon = null;
                // Определяем, какой из талонов надо использовать
                switch (mediaresource)
                {
                    case "Маяк":
                        talon = c.Талон_Маяк;
                        break;
                    case "Вести ФМ":
                        talon = c.Талон_Вести_ФМ;
                        break;
                    case "Радио России":
                        talon = c.Талон_Радио_России;
                        break;
                    case "Россия 1":
                        talon = c.Талон_Россия_1;
                        break;
                    case "Россия 24":
                        talon = c.Талон_Россия_24;
                        break;
                }
                // Для общей длительности в Итого
                if (talon != null)
                {
                    duration += talon.GetDurationTalonRecords();
                    durationCommonRecords += talon.GetDurationCommonRecords();
                }
                // Если у кандидата есть фамилия и этот талон (запасная проверка)
                if (c.View.Фамилия != "" && talon != null)
                {
                    // Делаем строку кандидата
                    tr = CreateRowProtocolCandidates(c, talon, mediaresource, i, cell5Text, settings.View.Кандидаты_Дата);
                }
                //
                if (tr == null) continue;
                // Добавляем к таблице
                table.Append(tr);
            }
            // Строка "Итого"
            tr = new TableRow();
            tc1 = new TableCell(WordDocument.CreateParagraph($"Итого"));
            tc2 = new TableCell(WordDocument.CreateParagraph($""));
            if (durationCommonRecords != TimeSpan.Zero)
            {
                tc3 = new TableCell(WordDocument.CreateParagraph($"{durationCommonRecords}"));
            }
            else
            {
                tc3 = new TableCell(WordDocument.CreateParagraph($""));
            }
            if (duration != TimeSpan.Zero)
            {
                tc4 = new TableCell(WordDocument.CreateParagraph($"{duration}"));
            }
            else
            {
                tc4 = new TableCell(WordDocument.CreateParagraph($""));
            }
            tc5 = new TableCell(WordDocument.CreateParagraph($""));
            tc6 = new TableCell(WordDocument.CreateParagraph($""));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
            table.Append(tr);
            // Возвращаем
            return table;
        }

        private static TableRow CreateRowProtocolCandidates(
            Candidate candidate, 
            Talon talon, 
            string mediaresource, 
            int i, 
            string row5Text,
            string row6Text)
        {
            // Формируем текст ячейки с талоном
            List<string> lines =
            [
                // Добавляем номер талона
                $"Талон № {talon.Id}"
            ];
            //
            foreach (var row in talon.TalonRecords)
            {
                lines.Add($"{row.Date} {row.Time} {row.Duration} {row.Description}");
            }
            // Текст ячейки с общим вещанием
            List<string> linesCommonRecords = new List<string>();
            //
            foreach (var row in talon.CommonRecords)
            {
                linesCommonRecords.Add($"{row.Date} {row.Time} {row.Duration} {row.Description}");
            }
            // Строка с данными
            var tr = new TableRow();
            //// Чтобы не разделялась при переходе на другую страницу
            //var rowProp = new TableRowProperties(new CantSplit());
            //tr.Append(rowProp);
            // 
            var tc1 = new TableCell(WordDocument.CreateParagraph($"{i + 1}"));
            var tc2 = new TableCell(WordDocument.CreateParagraph($"{candidate.View.Фамилия}" +
                $" {candidate.View.Имя} {candidate.View.Отчество}, {candidate.View.Округ_Номер}"));
            var tc3 = new TableCell(WordDocument.CreateParagraph(linesCommonRecords));
            var tc4 = new TableCell(WordDocument.CreateParagraph(lines));
            //tc4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Pct, Width = "60" }));
            var tc5 = new TableCell(WordDocument.CreateParagraph($"{row5Text}"));
            var tc6 = new TableCell(WordDocument.CreateParagraph($"{row6Text}"));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
            //
            return tr;
        }

        
    }
}

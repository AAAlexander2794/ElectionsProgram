﻿using DocumentFormat.OpenXml.Wordprocessing;
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

            // По каждому округу делаем свои 5 протоколов на каждое СМИ
            foreach (var region in regions)
            {
                // Формируем путь к документу
                var resultPath = $"{protocolFolderPath}" + $"{region.Номер} {region.Название_Падеж_им}\\";
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
        /// Формирует файл протокола кандидатов
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
            // Новый протокол
            var document = new WordDocument(templatePath);
            // Заполняем поля слияния
            document.SetMergeFieldText("Наименование_СМИ", $"{fieldMedia}");
            document.SetMergeFieldText("ИО_Фамилия_предст_СМИ", $"{settings.Кандидаты_ИО_Фамилия_предст_СМИ}");
            document.SetMergeFieldText("Дата", $"{settings.Кандидаты_Дата}");
            document.SetMergeFieldText("ИО_Фамилия_члена_изб_ком", $"{settings.Кандидаты_ИО_Фамилия_члена_изб_ком}");
            //
            try
            {
                document.SetBookmarkText($"Талон", "");
                var table = CreateTableProtocolCandidates(protocol, mediaresource);
                document.SetBookmarkTable($"Талон", table);
            }
            catch { }
            //
            document.Save(resultPath + $"{fileName}");
            document.Close();
        }

        /// <summary>
        /// Захардкоженная таблица протокола кандидатов
        /// </summary>
        /// <param name="talon"></param>
        /// <returns></returns>
        Table CreateTableProtocolCandidates(ProtocolCandidates protocol, string mediaresource)
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
            //// Now we create a new layout and make it "fixed".
            //TableLayout tl = new TableLayout() { Type = TableLayoutValues.Fixed };
            //tblProp.TableLayout = tl;
            //
            table.Append(tblProp);
            // Заголовки таблицы
            TableRow trHead = new TableRow();
            var tcH4 = new TableCell(CreateParagraph($"Даты и время\r\n" +
                $"выхода в эфир предвыборных\r\n" +
                $"агитационных материалов\r\n" +
                $"(число, месяц, год; время;\r\n" +
                $"количество\r\n" +
                $"минут/секунд"));
            tcH4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
            trHead.Append(
                new TableCell(CreateParagraph($"№ п/п")),
                new TableCell(CreateParagraph($"Фамилия, инициалы\r\n" +
                $"зарегистрированного кандидата,\r\n" +
                $"№ одномандатного\r\n" +
                $"избирательного округа, по\r\n" +
                $"которому он зарегистрирован")),
                new TableCell(CreateParagraph($"Даты и время выхода в эфир\r\n" +
                $"совместных агитационных\r\n" +
                $"мероприятий\r\n" +
                $"(число, месяц, год; время;\r\n" +
                $"количество\r\n" +
                $"минут/секунд")),
                tcH4,
                new TableCell(CreateParagraph($"Фамилия, инициалы представителя\r\n" +
                $"зарегистрированного кандидата,\r\n" +
                $"участвовавшего\r\n" +
                $"в жеребьевке (члена\r\n" +
                $"соответствующей\r\n" +
                $"избирательной комиссии с\r\n" +
                $"правом решающего голоса)")),
                new TableCell(CreateParagraph($"Подпись зарегистрированного кандидата,\r\n" +
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
            TableCell tc1 = new TableCell(CreateParagraph($"1"));
            TableCell tc2 = new TableCell(CreateParagraph($"2"));
            TableCell tc3 = new TableCell(CreateParagraph($"3"));
            TableCell tc4 = new TableCell(CreateParagraph($"4"));
            TableCell tc5 = new TableCell(CreateParagraph($"5"));
            TableCell tc6 = new TableCell(CreateParagraph($"6"));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
            table.Append(tr);
            // Добавляем округ одной строкой
            tr = CreateRowMergedCells(protocol.Округ, 6);
            table.Append(tr);
            // Для строки Итого со всех кандидатов длительность берем
            TimeSpan duration = TimeSpan.Zero;
            // По каждому кандидату из протокола
            for (int i = 0; i < protocol.Candidates.Count; i++)
            {
                //
                var c = protocol.Candidates[i];
                //
                string cell5Text = "";
                if (c.Info.Явка_кандидата == "1")
                {
                    // Если кандидат внесен (вообще и так должен быть внесен)
                    if (c.Info.Фамилия.Length > 0 &&
                    c.Info.Имя.Length > 0 &&
                    c.Info.Отчество.Length > 0)
                    {
                        cell5Text = $"{c.Info.Фамилия} {c.Info.Имя[0]}. {c.Info.Отчество[0]}.";
                    }
                }
                else if (c.Info.Явка_представителя == "1")
                {
                    // Если Представитель внесен
                    if (c.Info.Представитель_Фамилия.Length > 0 &&
                    c.Info.Представитель_Имя.Length > 0 &&
                    c.Info.Представитель_Отчество.Length > 0)
                    {
                        cell5Text = $"{c.Info.Представитель_Фамилия} {c.Info.Представитель_Имя[0]}. {c.Info.Представитель_Отчество[0]}.";
                    }
                }
                else
                {
                    cell5Text = $"{protocol.Изб_ком_Фамилия_ИО}";
                }
                //
                Talon talon = null;
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
                if (talon != null && talon.TotalDuration != null) duration += talon.TotalDuration;
                // Делаем строку кандидата
                tr = CreateRowProtocolCandidates(c, talon, mediaresource, i, cell5Text);
                //
                if (tr == null) continue;
                // Добавляем к таблице
                table.Append(tr);
            }
            // Строка "Итого"
            tr = new TableRow();
            tc1 = new TableCell(CreateParagraph($"Итого"));
            tc2 = new TableCell(CreateParagraph($""));
            tc3 = new TableCell(CreateParagraph($""));
            if (duration != TimeSpan.Zero)
            {
                tc4 = new TableCell(CreateParagraph($"{duration}"));
            }
            else
            {
                tc4 = new TableCell(CreateParagraph($""));
            }
            tc5 = new TableCell(CreateParagraph($""));
            tc6 = new TableCell(CreateParagraph($""));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
            table.Append(tr);
            // Возвращаем
            return table;
        }

        private TableRow CreateRowProtocolCandidates(Candidate candidate, Talon talon, string mediaresource, int i, string row5Text)
        {
            var tr = new TableRow();

            //
            if (candidate.Info.Фамилия == "") return null;
            // Формируем текст ячейки с талоном
            List<string> lines = new List<string>();
            //
            if (talon != null)
            {
                // Добавляем номер талона
                lines.Add($"Талон № {talon.Id}");
                //
                foreach (var row in talon.TalonRecords)
                {
                    if (talon.MediaResource == "Вести ФМ")
                    {
                        lines.Add($"{row.Date} {row.Time}:{row.Time.Second} {row.Duration} {row.Description}");
                    }
                    else
                    {
                        lines.Add($"{row.Date} {row.Time} {row.Duration} {row.Description}");
                    }
                }
            }
            else
            {
                lines.Add("");
            }
            // Строка с данными
            tr = new TableRow();
            //// Чтобы не разделялась при переходе на другую страницу
            //var rowProp = new TableRowProperties(new CantSplit());
            //tr.Append(rowProp);
            // 
            var tc1 = new TableCell(CreateParagraph($"{i + 1}"));
            var tc2 = new TableCell(CreateParagraph($"{candidate.Info.Фамилия} {candidate.Info.Имя} {candidate.Info.Отчество}, {candidate.Info.Округ_Номер}"));
            var tc3 = new TableCell(CreateParagraph($""));
            var tc4 = new TableCell(CreateParagraph(lines));
            //tc4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Pct, Width = "60" }));
            var tc5 = new TableCell(CreateParagraph($"{row5Text}"));
            var tc6 = new TableCell(CreateParagraph($""));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
            //
            return tr;
        }

        /// <summary>
        /// Объединяет указанное количество ячеек в строке вместе, также вставляет туда текст. (в теории, пока нет)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cellsNumber"></param>
        /// <returns></returns>
        private TableRow CreateRowMergedCells(string text, int cellsNumber)
        {
            var tr = new TableRow();
            // Создаем свойства ячейки для начала объединения
            TableCellProperties propStart = new TableCellProperties();
            propStart.Append(new HorizontalMerge()
            {
                Val = MergedCellValues.Restart,
            });
            // Делаем ячейку с текстом и добавляем ей свойство начала объединения
            var tc = new TableCell(CreateParagraph($"{text}", "alignmentCenter"));
            tc.Append(propStart);
            tr.Append(tc);
            // Цикл по количеству ячеек, которые надо объединить
            for (int i = 1; i < cellsNumber; i++)
            {
                // Создаем свойства ячейки для продолжения объединения
                var prop = new TableCellProperties();
                prop.Append(new HorizontalMerge()
                {
                    Val = MergedCellValues.Continue
                });
                // Создаем новую ячейку
                var tcNext = new TableCell(CreateParagraph($""));
                // Прикрепляем к новой ячейке свойства продолжения объединения
                tcNext.Append(prop);
                // Добавляем ячейку к строке
                tr.Append(tcNext);
            };
            //
            return tr;
        }
    }
}

using DocumentFormat.OpenXml.Wordprocessing;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Builders
{
    public static class ProtocolBuilder
    {
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// По каждому СМИ по каждой партии делаем отдельный протокол.
        /// Сортируем в подкаталоги партии, в нем по 5 протоколов для каждой СМИ.
        /// </remarks>
        /// <returns></returns>
        public static void CreateProtocolsParties(List<Party> parties, SettingsForProtocols settingsForProtocols)
        {
            var _folderPath = $"Документы/Протоколы/{DateTime.Now.ToString().Replace(":", "_")}\\";
            var _templatePath = Settings.Default.Protocols_TemplateFilePath_Parties;
            var _protocolsFilePath = Settings.Default.Protocols_FilePath;
            //
            List<WordDocument> protocols = new List<WordDocument>();
            // По каждой партии
            foreach (var party in parties)
            {
                try
                {
                    // Если не отмечено на печать, пропускаем
                    if (party.View.На_печать == "") continue;
                    // Формируем путь к документу
                    var resultPath = $"{_folderPath}" + $"{party.View.Название_условное}\\";
                    // Создает путь для документов, если вдруг каких-то папок нет
                    Directory.CreateDirectory(resultPath);
                    // По каждому СМИ
                    protocols.Add(CreateProtocol(party, settingsForProtocols, _templatePath, resultPath, "Маяк"));
                    protocols.Add(CreateProtocol(party, settingsForProtocols, _templatePath, resultPath, "Вести ФМ"));
                    protocols.Add(CreateProtocol(party, settingsForProtocols, _templatePath, resultPath, "Радио России"));
                    protocols.Add(CreateProtocol(party, settingsForProtocols, _templatePath, resultPath, "Россия 1"));
                    protocols.Add(CreateProtocol(party, settingsForProtocols, _templatePath, resultPath, "Россия 24"));
                }
                catch
                {
                    throw new Exception($"Ошибка с {party.View.Название_условное}");
                }
            }
            return;
        }

        /// <summary>
        /// Формирует файл протокола партии
        /// </summary>
        /// <param name="party"></param>
        /// <param name="templatePath"></param>
        /// <param name="resultPath"></param>
        /// <param name="mediaresource"></param>
        private static void CreateProtocol(Party party, SettingsForProtocols settings, string templatePath, string resultPath, string mediaresource)
        {
            //
            var partyName = $"{party.View.Название_полное}";
            // Фамилия И.О. человека, который подписывает талон в протоколе
            string personName = "";
            // Если представитель партии явился (и тянул талон)
            if (party.View.Явка == "1")
            {
                personName = party.Представитель_Фамилия_ИО;   
            }
            // Если представитель партии не явился, подписывает член избиркома
            else
            {
                personName = $"{settings.View.Партии_Член_изб_ком_Фамилия_ИО}";
            }
            //
            string fieldMedia = "";
            string fileName = "_";
            //
            Table table = null;
            //
            switch (mediaresource)
            {
                case "Маяк":
                    fieldMedia = settings.View.Название_СМИ_Маяк;
                    fileName = "Маяк.docx";
                    table = CreateTableParty(party.Талон_Маяк, partyName, personName, "00:23:45");
                    break;
                case "Вести ФМ":
                    fieldMedia = settings.View.Название_СМИ_Вести_ФМ;
                    fileName = "Вести ФМ.docx";
                    table = CreateTableParty(party.Талон_Вести_ФМ, partyName, personName, "00:11:00");
                    break;
                case "Радио России":
                    fieldMedia = settings.View.Название_СМИ_Радио_России;
                    fileName = "Радио России.docx";
                    table = CreateTableParty(party.Талон_Радио_России, partyName, personName, "00:23:45");
                    break;
                case "Россия 1":
                    fieldMedia = settings.View.Название_СМИ_Россия_1;
                    fileName = "Россия 1.docx";
                    table = CreateTableParty(party.Талон_Россия_1, partyName, personName, "00:23:45");
                    break;
                case "Россия 24":
                    fieldMedia = settings.View.Название_СМИ_Россия_24;
                    fileName = "Россия 24.docx";
                    table = CreateTableParty(party.Талон_Россия_24, partyName, personName, "00:23:45");
                    break;

            }
            // Создаем новый документ протокола на основе шаблона
            var document = new WordDocument(templatePath);
            // Заполняем поля слияния
            document.SetMergeFieldText("Наименование_СМИ", $"{fieldMedia}");
            document.SetMergeFieldText("ИО_Фамилия_предст_СМИ", $"{settings.View.Партии_Предст_СМИ_ИО_Фамилия}");
            document.SetMergeFieldText("Дата", $"{settings.View.Партии_Дата}");
            document.SetMergeFieldText("ИО_Фамилия_члена_изб_ком", $"{settings.View.Партии_Член_изб_ком_ИО_Фамилия}");
            //
            document.SetBookmarkText($"Талон", "");
            document.SetBookmarkTable($"Талон", table);
            //
            document.Save(resultPath + $"{fileName}");
            document.Close();
        }

        /// <summary>
        /// Захардкоженная таблица протокола партии
        /// </summary>
        /// <param name="talon"></param>
        /// <returns></returns>
        static Table CreateTableParty(Talon? talon, 
            string lastRow2CellText = "", 
            string lastRow5CellText = "", 
            string durationCustom = "")
        {
            // Создаем таблицу
            Table table = new Table();
            // Создаем настройки таблицы
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
            table.Append(tblProp);
            // Заголовки таблицы
            TableRow trHead = new TableRow();
            //
            var tcH3 = new TableCell(WordDocument.CreateParagraph($"Даты и время выхода в эфир\r\n" +
                $"совместных агитационных\r\n" +
                $"мероприятий\r\n" +
                $"(число, месяц, год; время;\r\n" +
                $"количество\r\n" +
                $"минут/секунд"));
            tcH3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
            //
            var tcH4 = new TableCell(WordDocument.CreateParagraph($"Даты и время\r\n" +
                $"выхода в эфир предвыборных\r\n" +
                $"агитационных материалов\r\n" +
                $"(число, месяц, год; время;\r\n" +
                $"количество\r\n" +
                $"минут/секунд"));
            tcH4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
            trHead.Append(
                new TableCell(WordDocument.CreateParagraph($"№ п/п")),
                new TableCell(WordDocument.CreateParagraph($"Наименование избирательного объединения")),
                tcH3,
                tcH4,
                new TableCell(WordDocument.CreateParagraph($"Фамилия, инициалы\r\n" +
                $"представителя избирательного\r\n" +
                $"объединения, участвовавшего\r\n" +
                $"в жеребьевке (члена\r\n" +
                $"соответствующей\r\n" +
                $"избирательной комиссии с\r\n" +
                $"правом решающего голоса)")),
                new TableCell(WordDocument.CreateParagraph($"Подпись представителя\r\n" +
                $"избирательного объединения,\r\n" +
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
            // Формируем текст ячейки с талоном
            List<string> lines = new List<string>();
            // Если талон был, добавляем в таблицу информацию
            if (talon != null)
            {
                // Добавляем номер талона
                lines.Add($"Талон № {talon.Number}");
                //
                foreach (var row in talon.BroadcastsNominal)
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
                //if (talon.MediaResource == "Вести ФМ")
                //{
                //    var some = "";
                //}
            }
            else
            {
                lines.Add("");
            }
            // Строка с данными
            tr = new TableRow();
            //
            tc1 = new TableCell(CreateParagraph($""));
            tc2 = new TableCell(CreateParagraph($"{lastRow2CellText}"));
            if (linesCustom != null)
            {
                tc3 = new TableCell(CreateParagraph(linesCustom));
            }
            tc4 = new TableCell(CreateParagraph(lines));
            tc5 = new TableCell(CreateParagraph($"{lastRow5CellText}"));
            tc6 = new TableCell(CreateParagraph($""));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
            table.Append(tr);
            // Строка "Итого"
            tr = new TableRow();
            tc1 = new TableCell(CreateParagraph($"Итого"));
            tc2 = new TableCell(CreateParagraph($""));
            tc3 = new TableCell(CreateParagraph($"{durationCustom}"));
            if (talon != null && talon.TotalDuration != null && talon.TotalDuration != TimeSpan.Zero)
            {
                tc4 = new TableCell(CreateParagraph($"{talon.TotalDuration}"));
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


    }
    
}

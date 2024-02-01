using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Reports.Contracts
{
    public static class ContractPartyBuilder
    {
        public static int BuildContractsParties(
            List<Party> parties,
            string contractsFolderPath,
            string partiesContractTemplatePath_РВ,
            string partiesContractTemplatePath_ТВ)
        {
            // Дата и время для формирования одной папки 
            var subfolder = DateTime.Now.ToString().Replace(":", "_");
            //
            contractsFolderPath += $"\\{subfolder}";
            // Создает путь для документов, если вдруг каких-то папок нет
            Directory.CreateDirectory(contractsFolderPath);
            // Счетчик
            int count = 0;
            // Для каждой партии
            foreach (var party in parties)
            {
                // Если не отмечено на печать, пропускаем
                if (party.View.На_печать == "") continue;
                //
                count++;
                // Создаем договор РВ
                var document_РВ = new WordDocument(partiesContractTemplatePath_РВ);
                // Формируем има файла договора
                var filePath = $"{contractsFolderPath}" + $"\\{party.View.Название_условное}";
                // Устанавливаем значения текста для полей документа, кроме закладок (талонов)
                SetMergeFields(document_РВ, party);
                // Устанавливаем таблицы талонов по закладкам
                SetTables(document_РВ, party, "radio");
                // Сохраняем и закрываем
                document_РВ.Save(filePath + "_радио.docx");
                document_РВ.Close();
                // Повторяем для договора ТВ
                var document_ТВ = new WordDocument(partiesContractTemplatePath_ТВ);
                //
                SetMergeFields(document_ТВ, party);
                //
                SetTables(document_ТВ, party, "tele");
                //
                document_ТВ.Save(filePath + "_ТВ.docx");
                document_ТВ.Close();
            }
            return count;
        }

        /// <summary>
        /// Захардкоженное присваивание значений местам в документе для партий.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="p"></param>
        private static void SetMergeFields(WordDocument doc, Party p)
        {
            //
            doc.SetMergeFieldText("Номер_договора", $"{p.View.Договор_Номер}");
            doc.SetMergeFieldText("Дата_договора", $"{p.View.Договор_Дата}");
            //
            doc.SetMergeFieldText("Название", $"{p.View.Название_полное}");
            doc.SetMergeFieldText("Постановление", $"{p.View.Постановление}");
            //
            doc.SetMergeFieldText("Представитель_Фамилия", $"{p.View.Представитель_Фамилия_Падеж_род}");
            doc.SetMergeFieldText("Представитель_Имя", $"{p.View.Представитель_Имя_Падеж_род}");
            doc.SetMergeFieldText("Представитель_Отчество", $"{p.View.Представитель_Отчество_Падеж_род}");
            doc.SetMergeFieldText("Представитель_Доверенность", $"{p.View.Представитель_Доверенность}");
            doc.SetMergeFieldText("Представитель_ИО_Фамилия", $"{p.Представитель_ИО_Фамилия}");
            //
            doc.SetMergeFieldText("Нотариус_Фамилия", $"{p.View.Нотариус_Фамилия}");
            doc.SetMergeFieldText("Нотариус_Имя", $"{p.View.Нотариус_Имя}");
            doc.SetMergeFieldText("Нотариус_Отчество", $"{p.View.Нотариус_Отчество}");
            doc.SetMergeFieldText("Нотариус_Реестр", $"{p.View.Нотариус_Реестр}");
            doc.SetMergeFieldText("Нотариус_Город", $"{p.View.Нотариус_Город}");
            //
            doc.SetMergeFieldText("ИНН", $"{p.View.ИНН}");
            doc.SetMergeFieldText("КПП", $"{p.View.КПП}");
            doc.SetMergeFieldText("ОГРН", $"{p.View.ОГРН}");
            doc.SetMergeFieldText("Счет", $"{p.View.Банк_счет}");
            doc.SetMergeFieldText("Место_нахождения", $"{p.View.Место_нахождения}");
        }

        /// <summary>
        /// Захардкоженное присваивание таблиц заладкам в документе
        /// </summary>
        /// <remarks>Так как в двух местах будут таблицы размещаться, 
        /// а закладка только на одно место, то сделаны дубликаты закладок 
        /// (надо потом в MergeField переделать)</remarks>
        /// <param name="doc"></param>
        /// <param name="p"></param>
        /// <param name="mode"></param>
        private static void SetTables(WordDocument doc, Party p, string mode = "both")
        {
            Table table;
            Table table2;
            //
            doc.SetBookmarkText($"Талон_1", "");
            doc.SetBookmarkText($"Талон_2", "");
            doc.SetBookmarkText($"Талон_3", "");
            doc.SetBookmarkText($"Талон_4", "");
            doc.SetBookmarkText($"Талон_5", "");
            doc.SetBookmarkText($"Талон_1_2", "");
            doc.SetBookmarkText($"Талон_2_2", "");
            doc.SetBookmarkText($"Талон_3_2", "");
            doc.SetBookmarkText($"Талон_4_2", "");
            doc.SetBookmarkText($"Талон_5_2", "");
            //
            if (mode == "both" || mode == "radio")
            {
                //
                if (p.Талон_Маяк != null)
                {
                    table = WordDocument.CreateTable(p.Талон_Маяк, p.Талон_Маяк.CommonTalon);
                    table2 = WordDocument.CreateTable(p.Талон_Маяк, p.Талон_Маяк.CommonTalon);
                    doc.SetBookmarkTable($"Талон_1", table);
                    doc.SetBookmarkTable($"Талон_1_2", table2);
                }
                //
                if (p.Талон_Радио_России != null)
                {
                    table = WordDocument.CreateTable(p.Талон_Радио_России, p.Талон_Радио_России.CommonTalon);
                    table2 = WordDocument.CreateTable(p.Талон_Радио_России, p.Талон_Радио_России.CommonTalon);
                    doc.SetBookmarkTable($"Талон_2", table);
                    doc.SetBookmarkTable($"Талон_2_2", table2);
                }
                //
                if (p.Талон_Вести_ФМ != null)
                {
                    table = WordDocument.CreateTable(p.Талон_Вести_ФМ, p.Талон_Вести_ФМ.CommonTalon);
                    table2 = WordDocument.CreateTable(p.Талон_Вести_ФМ, p.Талон_Вести_ФМ.CommonTalon);
                    doc.SetBookmarkTable($"Талон_3", table);
                    doc.SetBookmarkTable($"Талон_3_2", table2);
                }
            }
            //
            if (mode == "both" || mode == "tele")
            {
                //
                if (p.Талон_Россия_1 != null)
                {
                    table = WordDocument.CreateTable(p.Талон_Россия_1, p.Талон_Россия_1.CommonTalon);
                    table2 = WordDocument.CreateTable(p.Талон_Россия_1, p.Талон_Россия_1.CommonTalon);
                    doc.SetBookmarkTable($"Талон_4", table);
                    doc.SetBookmarkTable($"Талон_4_2", table2);
                }
                //
                if (p.Талон_Россия_24 != null)
                {
                    table = WordDocument.CreateTable(p.Талон_Россия_24, p.Талон_Россия_24.CommonTalon);
                    table2 = WordDocument.CreateTable(p.Талон_Россия_24, p.Талон_Россия_24.CommonTalon);
                    doc.SetBookmarkTable($"Талон_5", table);
                    doc.SetBookmarkTable($"Талон_5_2", table2);
                }
            }
        }
    }
}

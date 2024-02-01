using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Reports.Contracts
{
    public static class ContractCandidateBuilder
    {
        public static int BuildContractsCandidates(
            List<Candidate> candidates,
            string contractsFolderPath,
            string candidatesContractTemplatePath_РВ,
            string candidatesContractTemplatePath_ТВ)
        {
            // Счетчик для возврата количества обработанных кандидатов
            int count = 0;
            // Дата и время для формирования одной папки 
            var subfolder = DateTime.Now.ToString().Replace(":", "_");
            //
            contractsFolderPath += $"\\{subfolder}";
            // Создает путь для документов, если вдруг каких-то папок нет
            Directory.CreateDirectory(contractsFolderPath);
            //
            foreach (var candidate in candidates)
            {
                // Если не отмечено на печать, пропускаем
                if (candidate.View.На_печать == "") continue;
                // Создаем путь к папке договоров кандидата
                string regionFolderPath = $"{contractsFolderPath}";
                // Если у кандидата есть округ (не пустой)
                if (candidate.View.Округ_Номер != "" && candidate.View.Округ_Название_падеж_им != "")
                {
                    // К пути папки договоров кандидата добавляем подпапку округа
                    regionFolderPath += $"\\{candidate.View.Округ_Номер.Trim()} {candidate.View.Округ_Название_падеж_им.Trim()}";
                }
                // Создает подпапку округа
                Directory.CreateDirectory(regionFolderPath);
                // Создаем договор РВ по шаблону
                var document = new WordDocument(candidatesContractTemplatePath_РВ);
                //
                var resultPath = $"{regionFolderPath}\\" +
                        $"{candidate.View.Фамилия.Trim()} {candidate.View.Имя.Trim()} {candidate.View.Отчество.Trim()}";
                // Устанавливаем значения текста для полей документа, кроме закладок
                SetMergeFields(document, candidate);
                //
                SetTables(document, candidate, "radio");
                // Сохраняем и закрываем
                document.Save(resultPath + "_радио.docx");
                document.Close();

                // Повторяем создание документа для договора ТВ
                document = new WordDocument(candidatesContractTemplatePath_ТВ);
                SetMergeFields(document, candidate);
                SetTables(document, candidate, "tele");
                document.Save(resultPath + "_ТВ.docx");
                document.Close();
                //
                count++;
            }
            return count;
        }


        /// <summary>
        /// Захардкоженное присваивание значений местам в документе.
        /// </summary>
        private static void SetMergeFields(WordDocument doc, Candidate c)
        {
            doc.SetMergeFieldText("Фамилия", $"{c.View.Фамилия}");
            doc.SetMergeFieldText("Имя", $"{c.View.Имя}");
            doc.SetMergeFieldText("Отчество", $"{c.View.Отчество}");
            doc.SetMergeFieldText("Номер_договора", $"{c.View.Договор_Номер}");
            doc.SetMergeFieldText("Дата_договора", $"{c.View.Договор_Дата}");
            doc.SetMergeFieldText("Округ_Название", $"{c.View.Округ_Название_падеж_дат}");
            doc.SetMergeFieldText("Округ_Номер", $"{c.View.Округ_Номер}");
            doc.SetMergeFieldText("Постановление", $"{c.View.Постановление}");
            doc.SetMergeFieldText("Фамилия_представителя_род_падеж", $"{c.View.Представитель_Фамилия}");
            doc.SetMergeFieldText("Имя_представителя_род_падеж", $"{c.View.Представитель_Имя}");
            doc.SetMergeFieldText("Отчество_представителя_род_падеж", $"{c.View.Представитель_Отчество}");
            doc.SetMergeFieldText("ИО_Фамилия", $"{c.ИО_Фамилия}");
            doc.SetMergeFieldText("ИО_Фамилия_предст", $"{c.Представитель_ИО_Фамилия}");
            doc.SetMergeFieldText("Доверенность_на_представителя", $"{c.View.Представитель_Доверенность}");
            doc.SetMergeFieldText("ИНН", $"{c.View.ИНН}");
            doc.SetMergeFieldText("Спец_изб_счет", $"{c.View.Счет_банка}");

        }

        /// <summary>
        /// Захардкоженное присваивание таблиц заладкам в документе
        /// </summary>
        /// <remarks>Так как в двух местах будут таблицы размещаться, 
        /// а закладка только на одно место, то сделаны дубликаты закладок 
        /// (надо потом в MergeField переделать)</remarks>
        /// <param name="doc"></param>
        /// <param name="c"></param>
        /// <param name="mode"></param>
        private static void SetTables(WordDocument doc, Candidate c, string mode = "both")
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
                if (c.Талон_Маяк != null)
                {
                    table = WordDocument.CreateTable(c.Талон_Маяк, c.Талон_Маяк.CommonTalon);
                    table2 = WordDocument.CreateTable(c.Талон_Маяк, c.Талон_Маяк.CommonTalon);
                    doc.SetBookmarkTable($"Талон_1", table);
                    doc.SetBookmarkTable($"Талон_1_2", table2);
                }
                //
                if (c.Талон_Радио_России != null)
                {
                    table = WordDocument.CreateTable(c.Талон_Радио_России, c.Талон_Радио_России.CommonTalon);
                    table2 = WordDocument.CreateTable(c.Талон_Радио_России, c.Талон_Радио_России.CommonTalon);
                    doc.SetBookmarkTable($"Талон_2", table);
                    doc.SetBookmarkTable($"Талон_2_2", table2);
                }
                //
                if (c.Талон_Вести_ФМ != null)
                {
                    table = WordDocument.CreateTable(c.Талон_Вести_ФМ, c.Талон_Вести_ФМ.CommonTalon);
                    table2 = WordDocument.CreateTable(c.Талон_Вести_ФМ, c.Талон_Вести_ФМ.CommonTalon);
                    doc.SetBookmarkTable($"Талон_3", table);
                    doc.SetBookmarkTable($"Талон_3_2", table2);
                }
            }
            //
            if (mode == "both" || mode == "tele")
            {
                //
                if (c.Талон_Россия_1 != null)
                {
                    table = WordDocument.CreateTable(c.Талон_Россия_1, c.Талон_Россия_1.CommonTalon);
                    table2 = WordDocument.CreateTable(c.Талон_Россия_1, c.Талон_Россия_1.CommonTalon);
                    doc.SetBookmarkTable($"Талон_4", table);
                    doc.SetBookmarkTable($"Талон_4_2", table2);
                }
                //
                if (c.Талон_Россия_24 != null)
                {
                    table = WordDocument.CreateTable(c.Талон_Россия_24, c.Талон_Россия_24.CommonTalon);
                    table2 = WordDocument.CreateTable(c.Талон_Россия_24, c.Талон_Россия_24.CommonTalon);
                    doc.SetBookmarkTable($"Талон_5", table);
                    doc.SetBookmarkTable($"Талон_5_2", table2);
                }
            }
        }
    }
}

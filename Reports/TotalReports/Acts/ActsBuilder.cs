using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace ElectionsProgram.Reports.TotalReports.Acts
{
    /// <summary>
    /// Создает Акты оказанных услуг и Справки по фактическому времени.
    /// </summary>
    /// <remarks>
    /// По каждому клиенту (партии/кандидату) строит по 2 документа (ТВ и РВ) 
    /// с данными из соответствующих плейлистов.
    /// </remarks>
    public static class ActsBuilder
    {
        public static void CreateActs(
            List<Party> parties, 
            List<Candidate> candidates, 
            Playlist playlist_Россия_1,
            Playlist playlist_Россия_24,
            Playlist playlist_Маяк,
            Playlist playlist_Вести_ФМ,
            Playlist playlist_Радио_России,
            string templateCandidatesPath_РВ,
            string templateCandidatesPath_ТВ,
            string templatePartiesPath_РВ,
            string templatePartiesPath_ТВ,
            string catalogPath)
        {
            // По каждому кандидату
            foreach (var candidate in candidates)
            {
                CreateActCandidate_ТВ(
                    candidate, 
                    playlist_Россия_1, 
                    playlist_Россия_24, 
                    templateCandidatesPath_ТВ, 
                    catalogPath);
                CreateActCandidate_РВ(
                    candidate,
                    playlist_Маяк,
                    playlist_Вести_ФМ,
                    playlist_Радио_России,
                    templateCandidatesPath_РВ,
                    catalogPath);
            }
        }

        private static void CreateActCandidate_ТВ(
            Candidate candidate, 
            Playlist playlist_Россия_1, 
            Playlist playlist_Россия_24, 
            string templateCandidatesPath_ТВ,
            string catalogPath)
        {
            //
            List<PlaylistRecord> list = new List<PlaylistRecord>();
            // Ищем клиента по фамилии
            var client = playlist_Россия_1.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list.Add(record);
            }
            // Повторяем для другого плейлиста
            client = playlist_Россия_24.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list.Add(record);
            }
            // Создает подпапку 
            Directory.CreateDirectory(catalogPath);
            // Создаем договор по шаблону
            var document = new WordDocument(templateCandidatesPath_ТВ);
            //
            SetMergeFields(document, candidate, CreateDataTable(list));
            //
            document.Save(catalogPath + $"\\{candidate.View.Фамилия}_ТВ.docx");
            document.Close();
        }

        private static void CreateActCandidate_РВ(
            Candidate candidate,
            Playlist playlist_Маяк,
            Playlist playlist_Вести_ФМ,
            Playlist playlist_Радио_России,
            string templateCandidatesPath_РВ,
            string catalogPath)
        {
            //
            List<PlaylistRecord> list = new List<PlaylistRecord>();
            // Ищем клиента по фамилии
            var client = playlist_Маяк.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list.Add(record);
            }
            // Повторяем для другого плейлиста
            client = playlist_Вести_ФМ.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list.Add(record);
            }
            // Повторяем для другого плейлиста
            client = playlist_Радио_России.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list.Add(record);
            }
            // Создает подпапку 
            Directory.CreateDirectory(catalogPath);
            // Создаем договор по шаблону
            var document = new WordDocument(templateCandidatesPath_РВ);
            //
            SetMergeFields(document, candidate, CreateDataTable(list));
            //
            document.Save(catalogPath + $"\\{candidate.View.Фамилия}_РВ.docx");
            document.Close();
        }

        /// <summary>
        /// Захардкоженное присваивание значений местам в документе.
        /// </summary>
        private static void SetMergeFields(WordDocument doc, Candidate c, DataTable dt)
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
            //
            doc.SetBookmarkText($"Талон_1", "");
            doc.SetBookmarkTable($"Талон_1", WordDocument.CreateTable(dt));
        }

        private static DataTable CreateDataTable(List<PlaylistRecord> playlistRecords)
        {
            DataTable dt = new DataTable();
            //
            dt.Columns.Add("Название радиоканала");
            dt.Columns.Add("Дата выхода в эфир");
            dt.Columns.Add("Время выхода в эфир");
            dt.Columns.Add("Хронометраж");
            dt.Columns.Add("Вид (форма) \r\nпредвыборной агитации (Материалы/Совместные агитационные мероприятия)\r\n");
            //
            foreach (PlaylistRecord record in playlistRecords)
            {
                dt.Rows.Add();
                //
                dt.Rows[dt.Rows.Count - 1][0] = record.View.MediaresourceName;
                dt.Rows[dt.Rows.Count - 1][1] = record.Date;
                if (record.Time != null)
                {
                    dt.Rows[dt.Rows.Count - 1][2] = record.Time;
                }
                else
                {
                    dt.Rows[dt.Rows.Count - 1][2] = record.View.Time;
                }
                dt.Rows[dt.Rows.Count - 1][3] = record.DurationActual;
                dt.Rows[dt.Rows.Count - 1][4] = record.View.BroadcastForm;
            }
            //
            return dt;
        }

        
    }
}

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
using System.Windows.Controls;

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
            string templateCandidatesPath,
            string templatePartiesPath,
            string catalogPath)
        {
            // Группируем строки по временным отрезкам "утро, день, вечер", чтобы как в протоколах
            ClientPlaylistRecordBuilder.GroupClientRecords(playlist_Россия_1);
            ClientPlaylistRecordBuilder.GroupClientRecords(playlist_Россия_24);
            // По каждому кандидату
            foreach (var candidate in candidates)
            {
                CreateActCandidate(
                    candidate, 
                    playlist_Россия_1, 
                    playlist_Россия_24, 
                    playlist_Маяк,
                    playlist_Вести_ФМ,
                    playlist_Радио_России,
                    templateCandidatesPath, 
                    catalogPath);
                
            }
            // По каждой партии
            foreach (var party in parties)
            {
                CreateActParty(
                    party,
                    playlist_Россия_1,
                    playlist_Россия_24,
                    playlist_Маяк,
                    playlist_Вести_ФМ,
                    playlist_Радио_России,
                    templatePartiesPath,
                    catalogPath);
            }
        }

        private static void CreateActCandidate(
            Candidate candidate, 
            Playlist playlist_Россия_1, 
            Playlist playlist_Россия_24, 
            Playlist playlist_Маяк,
            Playlist playlist_Вести_ФМ,
            Playlist playlist_Радио_России,
            string templateCandidatesPath,
            string catalogPath)
        {
            //
            List<PlaylistRecord> list_Россия_1 = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Россия_24 = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Маяк = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Вести_ФМ = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Радио_России = new List<PlaylistRecord>();
            // Россия-1
            var client = playlist_Россия_1.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list_Россия_1.Add(record);
            }
            // Россия-24
            client = playlist_Россия_24.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list_Россия_24.Add(record);
            }
            // Маяк
            client = playlist_Маяк.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list_Маяк.Add(record);
            }
            // Вести ФМ
            client = playlist_Вести_ФМ.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list_Вести_ФМ.Add(record);
            }
            // Радио России
            client = playlist_Радио_России.Clients.First(c => c.Name.Contains(candidate.View.Фамилия));
            foreach (var record in client.PlaylistRecords)
            {
                list_Радио_России.Add(record);
            }
            // Создает подпапку 
            Directory.CreateDirectory(catalogPath);
            // Создаем договор по шаблону
            var document = new WordDocument(templateCandidatesPath);
            //
            SetMergeFields(document, candidate);
            //
            var dt_Россия_1 = CreateDataTable(list_Россия_1);
            var dt_Россия_24 = CreateDataTable(list_Россия_24);
            var dt_Маяк = CreateDataTable(list_Маяк);
            var dt_Вести_ФМ = CreateDataTable(list_Вести_ФМ);
            var dt_Радио_России = CreateDataTable(list_Радио_России);
            //
            SetTables(
                document,
                dt_Россия_1,
                dt_Россия_24,
                dt_Маяк,
                dt_Вести_ФМ,
                dt_Радио_России);
            //
            document.Save(catalogPath + $"\\{candidate.View.Фамилия}.docx");
            document.Close();
        }

        private static void CreateActParty(
            Party party,
            Playlist playlist_Россия_1,
            Playlist playlist_Россия_24,
            Playlist playlist_Маяк,
            Playlist playlist_Вести_ФМ,
            Playlist playlist_Радио_России,
            string templatePartiesPath,
            string catalogPath)
        {
            //
            List<PlaylistRecord> list_Россия_1 = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Россия_24 = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Маяк = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Вести_ФМ = new List<PlaylistRecord>();
            List<PlaylistRecord> list_Радио_России = new List<PlaylistRecord>();
            // Ищем клиента по фамилии
            var client = playlist_Россия_1.Clients.First(c => c.Name.Contains(party.View.Название_условное));
            foreach (var record in client.PlaylistRecords)
            {
                list_Россия_1.Add(record);
            }
            // Повторяем для другого плейлиста
            client = playlist_Россия_24.Clients.First(c => c.Name.Contains(party.View.Название_условное));
            foreach (var record in client.PlaylistRecords)
            {
                list_Россия_24.Add(record);
            }
            // Ищем клиента по фамилии
            client = playlist_Маяк.Clients.First(c => c.Name.Contains(party.View.Название_условное));
            foreach (var record in client.PlaylistRecords)
            {
                list_Маяк.Add(record);
            }
            // Повторяем для другого плейлиста
            client = playlist_Вести_ФМ.Clients.First(c => c.Name.Contains(party.View.Название_условное));
            foreach (var record in client.PlaylistRecords)
            {
                list_Вести_ФМ.Add(record);
            }
            // Повторяем для другого плейлиста
            client = playlist_Радио_России.Clients.First(c => c.Name.Contains(party.View.Название_условное));
            foreach (var record in client.PlaylistRecords)
            {
                list_Радио_России.Add(record);
            }
            // Создает подпапку 
            Directory.CreateDirectory(catalogPath);
            // Создаем договор по шаблону
            var document = new WordDocument(templatePartiesPath);
            //
            SetMergeFields(document, party);
            //
            var dt_Россия_1 = CreateDataTable(list_Россия_1);
            var dt_Россия_24 = CreateDataTable(list_Россия_24);
            var dt_Маяк = CreateDataTable(list_Маяк);
            var dt_Вести_ФМ = CreateDataTable(list_Вести_ФМ);
            var dt_Радио_России = CreateDataTable(list_Радио_России);
            //
            SetTables(
                document,
                dt_Россия_1,
                dt_Россия_24,
                dt_Маяк,
                dt_Вести_ФМ,
                dt_Радио_России);
            //
            document.Save(catalogPath + $"\\{party.View.Название_условное}.docx");
            document.Close();
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
            doc.SetMergeFieldText("Фамилия_ИО_предст", $"{c.Представитель_Фамилия_ИО}");
            doc.SetMergeFieldText("Доверенность_на_представителя", $"{c.View.Представитель_Доверенность}");
            doc.SetMergeFieldText("ИНН", $"{c.View.ИНН}");
            doc.SetMergeFieldText("Спец_изб_счет", $"{c.View.Счет_банка}");
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
            doc.SetMergeFieldText("Кандидат_Фамилия", $"{p.View.Кандидат_Фамилия_Падеж_род}");
            doc.SetMergeFieldText("Кандидат_Имя", $"{p.View.Кандидат_Имя_Падеж_род}");
            doc.SetMergeFieldText("Кандидат_Отчество", $"{p.View.Кандидат_Отчество_Падеж_род}");
            doc.SetMergeFieldText("Кандидат_Постановление", $"{p.View.Кандидат_Постановление}");
            //
            doc.SetMergeFieldText("Представитель_Фамилия", $"{p.View.Представитель_Фамилия_Падеж_род}");
            doc.SetMergeFieldText("Представитель_Имя", $"{p.View.Представитель_Имя_Падеж_род}");
            doc.SetMergeFieldText("Представитель_Отчество", $"{p.View.Представитель_Отчество_Падеж_род}");
            doc.SetMergeFieldText("Представитель_Доверенность", $"{p.View.Представитель_Доверенность}");
            doc.SetMergeFieldText("Представитель_ИО_Фамилия", $"{p.Представитель_ИО_Фамилия}");
            doc.SetMergeFieldText("Представитель_Фамилия_ИО", $"{p.Представитель_Фамилия_ИО}");
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

        private static void SetTables(
            WordDocument doc,
            DataTable dt_Россия_1, 
            DataTable dt_Россия_24, 
            DataTable dt_Маяк, 
            DataTable dt_Вести_ФМ, 
            DataTable dt_Радио_России)
        {
            //
            doc.SetBookmarkText($"Россия_1", "");
            doc.SetBookmarkTable($"Россия_1", WordDocument.CreateTable(dt_Россия_1));
            //
            doc.SetBookmarkText($"Россия_24", "");
            doc.SetBookmarkTable($"Россия_24", WordDocument.CreateTable(dt_Россия_24));
            //
            doc.SetBookmarkText($"Маяк", "");
            doc.SetBookmarkTable($"Маяк", WordDocument.CreateTable(dt_Маяк));
            //
            doc.SetBookmarkText($"Вести_ФМ", "");
            doc.SetBookmarkTable($"Вести_ФМ", WordDocument.CreateTable(dt_Вести_ФМ));
            //
            doc.SetBookmarkText($"Радио_России", "");
            doc.SetBookmarkTable($"Радио_России", WordDocument.CreateTable(dt_Радио_России));
        }

        private static DataTable CreateDataTable(List<PlaylistRecord> playlistRecords)
        {
            DataTable dt = new DataTable();
            //
            dt.Columns.Add("Название канала");
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
                    // dt.Rows[dt.Rows.Count - 1][2] = record.Time;
                    // Для выборов президента 2024
                    dt.Rows[dt.Rows.Count - 1][2] = record.View.Time;
                }
                else
                {
                    dt.Rows[dt.Rows.Count - 1][2] = record.View.Time;
                }
                if (record.DurationActual != null)
                {
                    dt.Rows[dt.Rows.Count - 1][3] = record.DurationActual.Value.TotalSeconds;
                }
                else
                {
                    dt.Rows[dt.Rows.Count - 1][3] = "";
                }
                dt.Rows[dt.Rows.Count - 1][4] = record.View.BroadcastForm;
            }
            //
            return dt;
        }

        
    }
}

using ClosedXML.Excel;
using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Builders
{
    public static class PlaylistBuilder
    {
        public static void BuildTable(List<Party> parties, List<Candidate> candidates, string catalogPath)
        {
            
            //
            var broadcastRecords = BuildBroadcastRecords(parties, candidates);
            //
            string subCatalog = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}";
            //
            WriteBroadcastRecordsToExcel(broadcastRecords, $@"{catalogPath}Рабочие\{subCatalog}\Маяк.xlsx", "Маяк");
            WriteBroadcastRecordsToExcel(broadcastRecords, $@"{catalogPath}Рабочие\{subCatalog}\Радио России.xlsx", "Радио России");
            WriteBroadcastRecordsToExcel(broadcastRecords, $@"{catalogPath}Рабочие\{subCatalog}\Вести ФМ.xlsx", "Вести ФМ");
            WriteBroadcastRecordsToExcel(broadcastRecords, $@"{catalogPath}Рабочие\{subCatalog}\Россия 1.xlsx", "Россия 1");
            WriteBroadcastRecordsToExcel(broadcastRecords, $@"{catalogPath}Рабочие\{subCatalog}\Россия 24.xlsx", "Россия 24");
        }

        static DataTable WriteBroadcastRecordsToExcel(List<PlaylistRecord> records, string filePath, string mediaResource)
        {
            //
            DataTable dt = new DataTable();
            // Заголовки таблицы
            dt.Columns.Add("Канал");
            dt.Columns.Add("Дата");
            dt.Columns.Add("Отрезок");
            dt.Columns.Add("Хрон");
            dt.Columns.Add("Округ");
            dt.Columns.Add("Партия/кандидат");
            dt.Columns.Add("Название партии/ФИО кандидата");
            dt.Columns.Add("Факт время");
            dt.Columns.Add("Форма выступления");
            dt.Columns.Add("Название ролика/тема дебатов");
            // Оставляем записи только заданного медиаресурса
            records = records.Where(x => x.View.MediaresourceName == mediaResource).ToList();
            //
            foreach (var record in records)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1][0] = record.View.MediaresourceName;
                dt.Rows[dt.Rows.Count - 1][1] = record.View.Date;
                dt.Rows[dt.Rows.Count - 1][2] = record.View.Time;
                dt.Rows[dt.Rows.Count - 1][3] = record.View.DurationNominal;
                dt.Rows[dt.Rows.Count - 1][4] = record.View.Region;
                dt.Rows[dt.Rows.Count - 1][5] = record.View.ClientType;
                dt.Rows[dt.Rows.Count - 1][6] = record.View.ClientName;
                dt.Rows[dt.Rows.Count - 1][7] = "";
                dt.Rows[dt.Rows.Count - 1][8] = "";
                dt.Rows[dt.Rows.Count - 1][9] = "";
            }
            // Запись в файл Excel
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "Отчет");
            wb.SaveAs(filePath);
            //
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Будущий я, прости меня
        /// </remarks>
        /// <param name="parties"></param>
        /// <param name="candidates"></param>
        /// <returns></returns>
        static List<PlaylistRecord> BuildBroadcastRecords(List<Party> parties, List<Candidate> candidates)
        {
            List<PlaylistRecord> records = new List<PlaylistRecord>();
            foreach (var party in parties)
            {
                var list1 = BuildBroadcastRecords(party, party.Талон_Россия_1);
                var list2 = BuildBroadcastRecords(party, party.Талон_Россия_24);
                var list3 = BuildBroadcastRecords(party, party.Талон_Радио_России);
                var list4 = BuildBroadcastRecords(party, party.Талон_Маяк);
                var list5 = BuildBroadcastRecords(party, party.Талон_Вести_ФМ);
                foreach (var r in list1) records.Add(r);
                foreach (var r in list2) records.Add(r);
                foreach (var r in list3) records.Add(r);
                foreach (var r in list4) records.Add(r);
                foreach (var r in list5) records.Add(r);
            }
            foreach (var candidate in candidates)
            {
                var list1 = BuildBroadcastRecords(candidate, candidate.Талон_Россия_1);
                var list2 = BuildBroadcastRecords(candidate, candidate.Талон_Россия_24);
                var list3 = BuildBroadcastRecords(candidate, candidate.Талон_Радио_России);
                var list4 = BuildBroadcastRecords(candidate, candidate.Талон_Маяк);
                var list5 = BuildBroadcastRecords(candidate, candidate.Талон_Вести_ФМ);
                foreach (var r in list1) records.Add(r);
                foreach (var r in list2) records.Add(r);
                foreach (var r in list3) records.Add(r);
                foreach (var r in list4) records.Add(r);
                foreach (var r in list5) records.Add(r);
            }
            //
            return records;
        }

        static List<PlaylistRecord> BuildBroadcastRecords(Party party, Talon talon)
        {
            List<PlaylistRecord> broadcastRecords = new List<PlaylistRecord>();
            if (talon == null) return broadcastRecords;
            foreach (var talonRecord in talon.TalonRecords)
            {
                PlaylistRecordView record = new PlaylistRecordView()
                {
                    MediaresourceName = talonRecord.MediaresourceName,
                    Date = talonRecord.Date.ToString(),
                    Time = talonRecord.Time.ToString(),
                    DurationNominal = talonRecord.Duration.ToString(),
                    ClientType = "Партия",
                    ClientName = party.View.Название_условное
                };
                broadcastRecords.Add(new PlaylistRecord(record));
            }
            return broadcastRecords;
        }

        static List<PlaylistRecord> BuildBroadcastRecords(Candidate candidate, Talon talon)
        {
            List<PlaylistRecord> broadcastRecords = new List<PlaylistRecord>();
            if (talon == null) return broadcastRecords;
            foreach (var talonRecord in talon.TalonRecords)
            {
                PlaylistRecordView record = new PlaylistRecordView()
                {
                    MediaresourceName = talonRecord.MediaresourceName,
                    Date = talonRecord.Date.ToString(),
                    Time = talonRecord.Time.ToString(),
                    DurationNominal = talonRecord.Duration.ToString(),
                    ClientType = "Кандидат",
                    Region = candidate.View.Округ_Номер,
                    ClientName = $"{candidate.View.Фамилия} " +
                        $"{candidate.View.Имя} " +
                        $"{candidate.View.Отчество} " +
                        $"({candidate.View.Округ_Номер})"
                };
                broadcastRecords.Add(new PlaylistRecord(record));
            }
            return broadcastRecords;
        }
    }
}

using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectionsProgram.Builders
{
    public static class TalonsBuilder
    {
        /// <summary>
        /// Парсинг таблицы конкретного вида [номер талона] [все строки талона]. Все талоны относятся к одному медиаресурсу.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="mediaResource"></param>
        /// <param name="dataTableCommon">Таблица с общими для всех клиентов вещаниями (данные только в одной строке, вторая ячейка)</param>
        /// <returns></returns>
        public static List<Talon> ParseTalonsVariantBase(DataTable dt, string mediaResource)
        {
            var talons = new List<Talon>();
            // В одной ячейке все строки одного талона
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var talonId = dt.Rows[i].Field<string>(0).Trim();
                // Из одной ячейки с текстом строим талон
                Talon talon = ParseTalonsVariantBase(mediaResource, talonId, dt.Rows[i].Field<string>(1));
                // Добавляем сформированный талон к результату
                talons.Add(talon);
            }
            return talons;
        }

        /// <summary>
        /// Парсинг текста одной ячейки в один талон.
        /// </summary>
        /// <param name="talonId"></param>
        /// <param name="mediaResource"></param>
        /// <param name="cellText"></param>
        /// <returns></returns>
        public static Talon ParseTalonsVariantBase(string mediaResource, string talonId, string cellText)
        {
            // Ячейка со строками одного талона парсится в список строк одного талона
            var talonRecords = ParseTalonString(talonId, mediaResource, cellText);
            // Создаем талон
            Talon talon = new Talon(mediaResource, talonId);
            // Все записи добавляем к талону
            foreach (var talonRecord in talonRecords)
            {
                talon.TalonRecords.Add(new TalonRecord(talonRecord));
            }
            //
            return talon;
        }

        /// <summary>
        /// КОСТЫЛЬ (очередной) для формирования талона общего вещания.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="mediaResource"></param>
        /// <param name="talonId"></param>
        /// <returns></returns>
        public static Talon? ParseTalonsVariantBase(DataTable dt, string mediaResource, string talonId)
        {
            if (dt.Rows.Count == 0) return null;
            
            return ParseTalonsVariantBase(mediaResource, talonId, dt.Rows[0].Field<string>(1));
        }

        /// <summary>
        /// Парсинг текста конкретного вида в записи талона.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mediaResource"></param>
        /// <param name="talonString">Текст из ячейки со всеми записями одного талона</param>
        /// <returns>Строки одного талона указанного медиаресурса.</returns>
        private static List<TalonRecordView> ParseTalonString(string id, string mediaResource, string talonString)
        {
            var result = new List<TalonRecordView>();
            //
            char[] delimitersRow = { '\n', '\r' };
            string[] rows = talonString.Split(delimitersRow);
            //
            char[] delimeterColumn = { ' ' };
            foreach (string row in rows)
            {
                //
                if (row.Length == 0) continue;
                var text = row;
                // Чистим от нескольких пробелов
                text = Regex.Replace(text, @"\s+", " ");
                //
                string[] columns = text.Split(delimeterColumn);
                //
                string description = "";
                if (columns.Length >= 4)
                {
                    description = columns[3];
                }
                //
                try
                {
                    result.Add(new TalonRecordView()
                    {
                        TalonNumber = id,
                        MediaresourceName = mediaResource,
                        Date = columns[0],
                        Time = columns[1],
                        Duration = columns[2],
                        Description = description
                    });
                }
                catch { continue; }
            }
            return result;
        }
    }
}


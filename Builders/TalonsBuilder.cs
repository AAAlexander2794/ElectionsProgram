﻿using ElectionsProgram.Entities;
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
        /// <returns></returns>
        public static List<BroadcastNominalView> ParseBroadcastNominalViews(DataTable dt, string mediaResource)
        {
            var result = new List<BroadcastNominalView>();
            // В одной ячейке все строки одного талона
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    var talonId = dt.Rows[i].Field<string>(0);
                    // Ячейка со строками одного талона парсится в список строк одного талона
                    var talonRecords = ParseTalonString(talonId, mediaResource, dt.Rows[i].Field<string>(1));
                    // Все записи добавляем к результату
                    foreach (var talonRecord in talonRecords)
                    {
                        result.Add(talonRecord);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка\r\n{ex.Message}\r\n(ParseBroadcastNominalViews)");
                }

            }
            return result;
        }

        /// <summary>
        /// Парсинг текста конкретного вида в записи талона.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mediaResource"></param>
        /// <param name="talonString">Текст из ячейки со всеми записями одного талона</param>
        /// <returns>Строки одного талона указанного медиаресурса.</returns>
        private static List<BroadcastNominalView> ParseTalonString(string id, string mediaResource, string talonString)
        {
            var result = new List<BroadcastNominalView>();
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
                    result.Add(new BroadcastNominalView()
                    {
                        TalonNumber = id,
                        MediaresourceName = mediaResource,
                        Date = columns[0],
                        Time = columns[1],
                        DurationNominal = columns[2],
                        Description = description
                    });
                }
                catch { continue; }
            }
            return result;
        }
    }
}
}

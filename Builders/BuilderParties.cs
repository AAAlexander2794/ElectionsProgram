using ElectionsProgram.Entities;
using ElectionsProgram.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectionsProgram.Builders
{
    /// <summary>
    /// Набор методов для работы с партиями.
    /// </summary>
    internal static class BuilderParties
    {
        public static List<Party> BuildParties(DataTable dataTable)
        {
            List<Party> parties = new List<Party>();
            // По строкам
            for (int i = 0; i < dataTable.Rows.Count - 1; i++)
            {
                // Создаем текстовое представление
                PartyView partyView = new PartyView();
                // Создаем партию с текстовым представлением (пока пустым)
                Party party = new Party(partyView);
                // По столбцам
                for (int j = 0; j < dataTable.Columns.Count - 1; j++)
                {
                    
                }
            }

            //
            return parties;
        }

        private static void GetFieldByName()
        {

        }
    }
}

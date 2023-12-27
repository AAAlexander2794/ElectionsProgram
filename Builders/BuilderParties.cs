using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
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
        
        public static List<Party> GetParties(DataTable dt)
        {
            List<Party> ls = new List<Party>();

            try
            {
                var list = dt.ToList<PartyView>();

                

                foreach (var item in list)
                {
                    ls.Add(new Party(item));
                }

                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ls;

            // Все имена столбцов
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            //
            //dto so all properties should be public
            var dtoProperties = typeof(Party).GetProperties();
            //
            foreach (DataRow row in dt.Rows)
            {
                // create  a new DTO object
                var item = new Party();

                // for each property of the dto
                foreach (var property in dtoProperties)
                {
                    var objPropName = property.Name;

                    
                    //var dbPropName = PartyView.Map.FirstOrDefault(property.Name);

                    //if (columnNames.Contains(dbPropName))
                    //{
                    //    if (row[dbPropName] != DBNull.Value)
                    //    {
                    //        // set the value
                    //        property.SetValue(item, row[dbPropName], null);
                    //    }
                    //}
                }

                // add the DTO to the list
                ls.Add(item);
            }
            return ls;
        }
       
    }

    

}

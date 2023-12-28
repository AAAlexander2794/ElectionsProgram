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

            
        }
       
    }

    

}

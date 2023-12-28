using ClosedXML.Excel;
using ElectionsProgram.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace ElectionsProgram.Processors
{
    /// <summary>
    /// Отвечает за чтение/запись файлов
    /// </summary>
    public static class ProcessorXML
    {
        //public static DataTable LoadDataFromExcel(string path = "")
        //{
        //    DataTable dataTable = new DataTable();

        //}

        //public static void SaveToExcel(DataTable dataTable, string path = "")
        //{
        //    // Запись в файл Excel
        //    XLWorkbook wb = new XLWorkbook();
        //    wb.Worksheets.Add(dataTable, "Отчет");
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    // Если путь не был задан, используем путь к приложению
        //    if (path == "")
        //    {
        //        saveFileDialog.InitialDirectory = AppContext.BaseDirectory;
        //    }
        //    if (saveFileDialog.ShowDialog() == true)
        //    {
        //        wb.SaveAs(saveFileDialog.FileName);
        //    }
        //}

        #region Взаимодействие с XML

        public static void SaveDBToXml(ViewModels.ViewModel electionsBase, string filePath, string fileName)
        {
            // Создаем директорию, если ее не было
            System.IO.Directory.CreateDirectory(filePath);
            //
            XmlSerializer xs = new XmlSerializer(typeof(ViewModels.ViewModel));
            using (TextWriter tw = new StreamWriter($"{filePath}\\{fileName}"))
            {
                xs.Serialize(tw, electionsBase);
            }
        }

        public static ViewModels.ViewModel LoadDBFromXml(string filePath, string fileName)
        {
            ViewModels.ViewModel _base;
            XmlSerializer xs = new XmlSerializer(typeof(ViewModels.ViewModel));
            using (var sr = new StreamReader($"{filePath}\\{fileName}"))
            {
                _base = (ViewModels.ViewModel)xs.Deserialize(sr);
            }
            return _base;
        }

        #endregion Взаимодействие с XML
    }
}

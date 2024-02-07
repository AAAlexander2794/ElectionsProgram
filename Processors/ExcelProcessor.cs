using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ElectionsProgram.Processors
{
    internal static class ExcelProcessor
    {
        /// <summary>
        /// Запись <see cref="DataTable"/> в Excel.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="path"></param>
        /// <param name="sheetName"></param>
        /// <remarks>
        /// Использует пакет ClosedXML.
        /// </remarks>
        public static void SaveToExcel(DataTable dataTable, string path = "", string sheetName = "Данные")
        {
            // Создаем новую книгу
            XLWorkbook wb = new XLWorkbook();
            // Добавляем к книге лист с данными
            wb.Worksheets.Add(dataTable, sheetName);
            // Диалоговое окно записи файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // Если путь не был задан, используем путь к приложению
            if (path == "")
            {
                saveFileDialog.InitialDirectory = AppContext.BaseDirectory;
                if (saveFileDialog.ShowDialog() == true)
                {
                    wb.SaveAs(saveFileDialog.FileName);
                }
                return;
            }
            else
            {
                wb.SaveAs(path);
                return;
            }
        }

        /// <summary>
        /// Загружаем данные с листа Excel.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetNumber"></param>
        /// <returns></returns>
        /// <remarks>
        /// Использует пакет ClosedXML.
        /// </remarks>
        public static DataTable LoadFromExcel(string filePath = "", int sheetNumber = 1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (filePath == "")
            {
                openFileDialog.InitialDirectory = AppContext.BaseDirectory;
                if (openFileDialog.ShowDialog() == true)
                {
                    filePath = openFileDialog.FileName;
                }
                else
                {
                    throw new Exception("Не указан путь к файлу Excel.");
                }
            }
            // Open the Excel file using ClosedXML.
            // Keep in mind the Excel file cannot be open when trying to read it
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                // Чтение листа Excel
                IXLWorksheet workSheet = workBook.Worksheet(sheetNumber);
                //
                DataTable dt = new DataTable();
                // Флаг первой строки (для заголовков)
                bool firstRow = true;
                // Номер первого и последнего столбца с данными
                int firstColumn = 0;
                int lastColumn = 0;
                //
                foreach (IXLRow row in workSheet.Rows())
                {
                    // В первой строке заголовки столбцов для DataTable.
                    if (firstRow)
                    {
                        // Определяем первую и последнюю ячейку по заголовкам
                        firstColumn = row.FirstCellUsed().Address.ColumnNumber;
                        lastColumn = row.LastCellUsed().Address.ColumnNumber;
                        // Начинаем с первой использованной ячейки, заканчиваем последней использованной.
                        foreach (IXLCell cell in row.Cells(firstColumn, lastColumn))
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                        
                    }
                    else
                    {
                        // Добавляем строку в DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        // Читаем те же столбцы, что и для заголовков
                        foreach (IXLCell cell in row.Cells(firstColumn, lastColumn))
                        {
                            // Чтобы не выдавало ошибку при попытке прочитать формулу
                            if (cell.HasFormula)
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = "formula";
                                continue;
                            }
                            //
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

                return dt;
            }
        }
    }
}

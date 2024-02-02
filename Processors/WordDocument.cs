using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Processors
{
    /// <summary>
    /// Файл формата .docx с методами обработки.
    /// </summary>
    /// <remarks>
    /// Не должен ничего знать про бизнес-логику.
    /// </remarks>
    public class WordDocument
    {
        private WordprocessingDocument Document { get; }

        public IDictionary<string, BookmarkStart> BookmarkMap { get; }

        public WordDocument(string templatePath)
        {
            Document = WordprocessingDocument.CreateFromTemplate(templatePath);
            BookmarkMap = GetBookmarkMap(Document);
        }

        public void Save(string path)
        {
            Document.SaveAs(path);
        }

        public void Close()
        {
            Document.Close();
        }

        /// <summary>
        /// Возвращает карту закладок файла .docx
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Словарь закладок файла .docx</returns>
        private static IDictionary<string, BookmarkStart> GetBookmarkMap(string filePath)
        {
            //
            IDictionary<string, BookmarkStart> bookmarkMap = new Dictionary<string, BookmarkStart>();
            //
            using (WordprocessingDocument document = WordprocessingDocument.CreateFromTemplate(filePath))
            {
                bookmarkMap = GetBookmarkMap(document);
            }
            //
            return bookmarkMap;
        }

        /// <summary>
        /// Возвращает карту закладок шаблона
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Словарь закладок шаблона</returns>
        private static IDictionary<string, BookmarkStart> GetBookmarkMap(WordprocessingDocument document)
        {
            //
            IDictionary<string, BookmarkStart> bookmarkMap = new Dictionary<string, BookmarkStart>();
            //
            foreach (BookmarkStart bookmarkStart in document.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarkMap[bookmarkStart.Name] = bookmarkStart;
            }
            //
            return bookmarkMap;
        }

        public void SetBookmarkText(string bookmarkName, string text)
        {
            try
            {
                SetBookmarkText(BookmarkMap[bookmarkName], text);
            }
            catch (Exception ex)
            {
                throw new Exception("Закладка не найдена.\n" + ex.Message);
            };
        }

        private static void SetBookmarkText(BookmarkStart bookmarkStart, string text)
        {
            Run bookmarkText = bookmarkStart.NextSibling<Run>();
            if (bookmarkText != null)
            {
                bookmarkText.GetFirstChild<Text>().Text = text;
            }
        }

        /// <summary>
        /// Вставляет таблицу после закладки
        /// </summary>
        /// <param name="bookmarkName"></param>
        /// <param name="table"></param>
        public void SetBookmarkTable(string bookmarkName, Table table)
        {
            if (table == null) return;
            var mainPart = Document.MainDocumentPart;
            var res = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
                      where bm.Name == bookmarkName
                      select bm;
            var bookmark = res.SingleOrDefault();
            if (bookmark != null)
            {
                // Родитель закладки
                var parent = bookmark.Parent;
                // 
                parent.InsertAfterSelf(table);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeFieldName">Название поля для слияния.</param>
        /// <param name="text"></param>
        public void SetMergeFieldText(string mergeFieldName, string text)
        {
            //
            string FieldDelimeter = " MERGEFIELD ";
            string FieldDelimeterEnd = " \\* MERGEFORMAT ";

            foreach (FieldCode field in Document.MainDocumentPart.RootElement.Descendants<FieldCode>())
            {
                var fieldNameStart = field.Text.LastIndexOf(FieldDelimeter, StringComparison.Ordinal);
                var fieldName = field.Text.Substring(fieldNameStart + FieldDelimeter.Length).Replace(FieldDelimeterEnd, "").Trim();

                if (fieldName == mergeFieldName)
                {
                    foreach (Run run in Document.MainDocumentPart.Document.Descendants<Run>())
                    {
                        foreach (Text txtFromRun in run.Descendants<Text>().Where(a => a.Text == $"«{fieldName}»"))
                        {
                            txtFromRun.Text = text;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Захардкоженная таблица талона
        /// </summary>
        /// <remarks>
        /// Надо переписать, чтобы опиралось на обезличинную DataTable.
        /// </remarks>
        /// <param name="talon"></param>
        /// <returns></returns>
        public static Table CreateTable(Talon talon, Talon? commonTalon = null)
        {
            if (talon == null) return null;
            // 
            Table table = new Table();
            //
            TableProperties tblProp = new TableProperties();
            TableBorders tblBorders = new TableBorders()
            {
                BottomBorder = new BottomBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                TopBorder = new TopBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                LeftBorder = new LeftBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                RightBorder = new RightBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                InsideHorizontalBorder = new InsideHorizontalBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                InsideVerticalBorder = new InsideVerticalBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                }
            };
            tblProp.Append(tblBorders);
            table.Append(tblProp);
            //
            TableRow trHead = new TableRow();

            //// test
            //trHead.Append(new TableCell(CreateParagraph($"Название радиоканала")));
            //trHead.Append(new TableCell(CreateParagraph($"Дата выхода в эфир")));
            //trHead.Append(new TableCell(CreateParagraph($"Время выхода \r\nв эфир")));
            //trHead.Append(new TableCell(CreateParagraph($"Хронометраж")));
            //    trHead.Append(new TableCell(CreateParagraph($"Вид (форма) предвыборной агитации\r\n" +
            //    $"(Материалы, Совместные агитационные мероприятия)"))
            //    );

            //
            trHead.Append(
                new TableCell(CreateParagraph($"Название радиоканала")),
                new TableCell(CreateParagraph($"Дата выхода в эфир")),
                new TableCell(CreateParagraph($"Время выхода \r\nв эфир")),
                new TableCell(CreateParagraph($"Хронометраж")),
                new TableCell(CreateParagraph($"Вид (форма) предвыборной агитации\r\n" +
                $"(Материалы, Совместные агитационные мероприятия)"))
                );
            //
            table.Append(trHead);
            //
            foreach (var row in talon.TalonRecords)
            {
                //
                TableRow tr = new TableRow();
                //
                TableCell tc1 = new TableCell(CreateParagraph($"{row.View.MediaresourceName}"));
                TableCell tc2 = new TableCell(CreateParagraph($"{row.View.Date}"));
                TableCell tc3 = new TableCell(CreateParagraph($"{row.View.Time}"));
                TableCell tc4 = new TableCell(CreateParagraph($"{row.View.Duration}"));
                TableCell tc5 = new TableCell(CreateParagraph($""));
                //
                tr.Append(tc1, tc2, tc3, tc4, tc5);
                //
                table.Append(tr);
            }
            // Если есть талон общего вещания
            if (commonTalon != null)
            {
                foreach (var row in commonTalon.TalonRecords)
                {
                    //
                    TableRow tr = new TableRow();
                    //
                    TableCell tc1 = new TableCell(CreateParagraph($"{row.View.MediaresourceName}"));
                    TableCell tc2 = new TableCell(CreateParagraph($"{row.View.Date}"));
                    TableCell tc3 = new TableCell(CreateParagraph($"{row.View.Time}"));
                    TableCell tc4 = new TableCell(CreateParagraph($"{row.View.Duration}"));
                    TableCell tc5 = new TableCell(CreateParagraph($"Совместные агитационные мероприятия"));
                    //
                    tr.Append(tc1, tc2, tc3, tc4, tc5);
                    //
                    table.Append(tr);
                }
            }
            return table;
        }

        /// <summary>
        /// Создает новый абзац текста
        /// </summary>
        /// <param name="text"></param>
        /// <param name="style">Для выбора различных дополнений текста типа выравнивания по центру</param>
        /// <returns></returns>
        public static Paragraph CreateParagraph(string text, string style = "default", string textSize = "12")
        {
            List<string> lines = new List<string>();
            //
            var some = text.Split("\r\n");
            for (int i = 0; i < some.Length; i++)
            {
                lines.Add(some[i]);
            }
            //
            return CreateParagraph(lines, textSize, style);
        }

        /// <summary>
        /// Создает новый абзац текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Paragraph CreateParagraph(List<string> lines, string textSize = "12", string style = "default")
        {
            var paragraph = new Paragraph();
            var run = new Run();
            // Если список строк есть и не пустой
            if (lines != null && lines.Count > 0)
            {
                // Добавляем без лишнего переноса на новую строку в конце
                for (int i = 0; i < lines.Count - 1; i++)
                {
                    run.AppendChild(new Text(lines[i]));
                    run.AppendChild(new Break());
                }
                run.AppendChild(new Text(lines[lines.Count - 1]));
            }
            //
            RunProperties runProperties = new RunProperties();
            FontSize size = new FontSize();
            size.Val = StringValue.FromString(textSize);
            runProperties.Append(size);
            //
            run.Append(runProperties);
            //
            if (style == "alignmentCenter")
            {
                Justification justification = new Justification()
                {
                    Val = JustificationValues.Center
                };
                var prProp = new ParagraphProperties();
                prProp.Append(justification);
                paragraph.Append(prProp);
            }
            paragraph.Append(run);
            //
            return paragraph;
        }

        /// <summary>
        /// Объединяет указанное количество ячеек в строке вместе, также вставляет туда текст. (в теории, пока нет)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cellsNumber"></param>
        /// <returns></returns>
        public static TableRow CreateRowMergedCells(string text, int cellsNumber)
        {
            var tr = new TableRow();
            // Создаем свойства ячейки для начала объединения
            TableCellProperties propStart = new TableCellProperties();
            propStart.Append(new HorizontalMerge()
            {
                Val = MergedCellValues.Restart,
            });
            // Делаем ячейку с текстом и добавляем ей свойство начала объединения
            var tc = new TableCell(CreateParagraph($"{text}", "alignmentCenter"));
            tc.Append(propStart);
            tr.Append(tc);
            // Цикл по количеству ячеек, которые надо объединить
            for (int i = 1; i < cellsNumber; i++)
            {
                // Создаем свойства ячейки для продолжения объединения
                var prop = new TableCellProperties();
                prop.Append(new HorizontalMerge()
                {
                    Val = MergedCellValues.Continue
                });
                // Создаем новую ячейку
                var tcNext = new TableCell(CreateParagraph($""));
                // Прикрепляем к новой ячейке свойства продолжения объединения
                tcNext.Append(prop);
                // Добавляем ячейку к строке
                tr.Append(tcNext);
            };
            //
            return tr;
        }

        /// <summary>
        /// Создает таблицу на основе данных из <see cref="DataTable"/>.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static Table CreateTable(DataTable dataTable, string textSize = "12")
        {
            // 
            Table table = new Table();
            //
            TableProperties tblProp = new TableProperties();
            TableBorders tblBorders = new TableBorders()
            {
                BottomBorder = new BottomBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                TopBorder = new TopBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                LeftBorder = new LeftBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                RightBorder = new RightBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                InsideHorizontalBorder = new InsideHorizontalBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                },
                InsideVerticalBorder = new InsideVerticalBorder()
                {
                    Size = 4,
                    Val = BorderValues.Single
                }
            };
            tblProp.Append(tblBorders);
            table.Append(tblProp);
            //
            TableRow trHead = new TableRow();
            foreach (DataColumn item in dataTable.Columns)
            {
                trHead.Append(new TableCell(CreateParagraph($"{item.Caption}", textSize)));
                // Размер ячейки по содержимому
                trHead.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
            }
            // Добавляем строку заголовков
            table.Append(trHead);
            // По всем строкам таблицы данных
            foreach (DataRow row in dataTable.Rows)
            {
                //
                TableRow tr = new TableRow();
                // По всем ячейкам строки
                foreach(var item in row.ItemArray)
                {
                    if (item == null)
                    {
                        // Добавляем пустую ячейку
                        tr.Append(new TableCell(CreateParagraph($"", textSize)));
                    }
                    else
                    {
                        tr.Append(new TableCell(CreateParagraph($"{item}", textSize)));
                        // Размер ячейки по содержимому
                        tr.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
                    }
                }
                //
                table.Append(tr);
            }
            //
            return table;
        }

    }
}

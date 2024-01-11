using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
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
        Table CreateTable(Talon talon)
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
            foreach (var row in talon.BroadcastsNominal)
            {
                //
                TableRow tr = new TableRow();
                //
                TableCell tc1 = new TableCell(CreateParagraph($"{row.View.MediaresourceName}"));
                TableCell tc2 = new TableCell(CreateParagraph($"{row.View.Date}"));
                TableCell tc3 = new TableCell(CreateParagraph($"{row.View.Time}"));
                TableCell tc4 = new TableCell(CreateParagraph($"{row.View.DurationNominal}"));
                TableCell tc5 = new TableCell(CreateParagraph($""));
                //
                tr.Append(tc1, tc2, tc3, tc4, tc5);
                //
                table.Append(tr);
            }
            return table;
        }

        /// <summary>
        /// Создает новый абзац текста
        /// </summary>
        /// <param name="text"></param>
        /// <param name="style">Для выбора различных дополнений текста типа выравнивания по центру</param>
        /// <returns></returns>
        public static Paragraph CreateParagraph(string text, string style = "default")
        {
            var paragraph = new Paragraph();
            var run = new Run();
            var runText = new Text($"{text}");
            //
            RunProperties runProperties = new RunProperties();
            FontSize size = new FontSize();
            size.Val = StringValue.FromString("18");
            runProperties.Append(size);
            //
            run.Append(runProperties);
            run.Append(runText);
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
            //
            paragraph.Append(run);
            //
            return paragraph;
        }

        /// <summary>
        /// Создает новый абзац текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Paragraph CreateParagraph(List<string> lines)
        {
            var paragraph = new Paragraph();
            var run = new Run();
            // Добавляем без лишнего переноса на новую строку в конце
            for (int i = 0; i < lines.Count - 1; i++)
            {
                run.AppendChild(new Text(lines[i]));
                run.AppendChild(new Break());
            }
            run.AppendChild(new Text(lines[lines.Count - 1]));
            //
            RunProperties runProperties = new RunProperties();
            FontSize size = new FontSize();
            size.Val = StringValue.FromString("18");
            runProperties.Append(size);
            //
            run.Append(runProperties);
            paragraph.Append(run);
            //
            return paragraph;
        }

    }
}

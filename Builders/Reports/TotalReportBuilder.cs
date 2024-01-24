using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectionsProgram.Builders.TotalReport
{
    public static partial class TotalReportBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlists">Плейлист одного медиаресурса</param>
        /// <param name="record">Запись из плейлиста со всеми медиаресурсами</param>
        private static void AddRecord(List<Playlist> playlists, PlaylistRecord record)
        {
            foreach (var playlist in playlists)
            {
                // Если такой плейлист уже есть в списке
                if (playlist.MediaresourceName == record.MediaResourceName)
                {
                    // Передаем на добавление записи клиентам найденного плейлиста
                    AddRecord(playlist.Clients, record);
                    // Запись присвоена, возвращаемся
                    return;
                }
            }
            
            // Если все плейлисты проверили, но не прекратили, значит, надо создать новый плейлист

            // Создаем новый плейлист
            Playlist newPlaylist = new Playlist()
            { 
                MediaresourceName = record.MediaResourceName
            };
            // Передаем новый плейлист на добавление ему записи
            AddRecord(newPlaylist.Clients, record);
            //
            playlists.Add(newPlaylist);
            //
            return;
        }

        private static Client AddRecord(List<Client> clients, PlaylistRecord record)
        {
            //
            foreach (var client in clients)
            {
                // Если клиент уже есть в списке
                if (client.Name == record.ClientName)
                {
                    // Добавляем запись из плейлиста клиенту
                    client.PlaylistRecords.Add(record);
                    // Запись присвоена, прекращаем
                    return client;
                }
            }

            // Если всех клиентов проверили, но не прекратили, значит, клиента в списке нет - добавляем

            // Создаем нового клиента по данным записи из плейлиста
            Client newClient = new Client()
            {
                Name = record.ClientName,
                Type = record.ClientType
            };
            // Добавляем новому клиенту запись
            newClient.PlaylistRecords.Add(record);
            // Добавляем нового клиента в список клиентов
            clients.Add(newClient);
            // Возвращаем
            return newClient;
        }


        public static void BuildTotalReport(DataTable playlist, string mediaResource, string subCatalog)
        {
            //string recordsFilePath = $@"./Настройки/Учет вещания/{mediaResource}.xlsx";

            // Из таблицы плейлиста формируем список текстового представления записей
            var playlistRecordsView = playlist.ToList<PlaylistRecordView>();
            // Список из 5 (ожидаемо) плейлистов по каждому каналу
            List<Playlist> mediaresourcePlaylists = new List<Playlist>();
            //
            foreach (var playlistRecord in playlistRecordsView)
            {
                bool isNewPlaylist = false;
                foreach(var item in mediaresourcePlaylists)
                {
                    if (item.MediaresourceName == playlistRecord.MediaresourceName)
                    {
                        
                    }
                }
                var newPlaylistRecord = new PlaylistRecord(playlistRecord);
            }
            //

            

            // На основе строк одной СМИ строим блоки для таблицы
            var blocks = BuildTotalReport(playlistRecords, regions);
            //
            foreach (var candidate in candidates)
            {
                bool isFound = false;
                // Поиск только по блокам, где указан округ, то есть только по кандидатам
                foreach (var block in blocks.Where(b => b.RegionNumber.All(char.IsDigit)))
                {
                    foreach (var client in block.ClientBlocks)
                    {
                        // Если нашли этого кандидата, добавляем информацию о договоре и переходим сразу к следующему
                        if (client.ClientName == $"{candidate.Info.Фамилия} {candidate.Info.Имя} {candidate.Info.Отчество}")
                        {
                            isFound = true;
                            client.ClientContract = $"Договор № {candidate.Info.Номер_договора} от {candidate.Info.Дата_договора} г.";
                            break;
                        }
                    }
                    if (isFound) break;
                }
            }
            var parties = builder.BuildParties("1");
            foreach (var party in parties)
            {
                bool isFound = false;
                foreach (var block in blocks.Where(b => b.RegionNumber == "–"))
                {
                    foreach (var client in block.ClientBlocks)
                    {
                        // Если нашли партию, добавляем информацию о договоре и переходим сразу к следующему
                        if (party.Info.Партия_Название_Рабочее.Contains(client.ClientName))
                        {
                            isFound = true;
                            client.ClientContract = $"Договор № {party.Info.Номер_договора} от {party.Info.Дата_договора} г.";
                            break;
                        }
                    }
                    if (isFound) break;
                }
            }
            //
            var templatePath = $@"./Настройки/Учет вещания/Форма учета.dotx";
            var resultPath = $@"./Документы/Отчеты/{subCatalog}/";
            // Сохраняем отчет в ворд
            CreateReport(blocks, templatePath, resultPath, mediaResource);


        }

        public List<ReportRegionBlock> BuildTotalReport(List<BroadcastRecord> broadcastRecords, List<Region> regions)
        {
            List<ReportRegionBlock> regionBlocks = new List<ReportRegionBlock>();
            // Заполняем регионы заранее в нужном порядке 
            foreach (var region in regions)
            {
                regionBlocks.Add(new ReportRegionBlock()
                {
                    RegionNumber = region.Number,
                    RegionCaption = region.Caption
                });
            }
            // По каждой строке вещания
            foreach (var broadcastRecord in broadcastRecords)
            {
                bool regionCreated = false;
                foreach (var region in regionBlocks)
                {
                    // Если такой регион уже есть
                    if (region.RegionNumber == broadcastRecord.RegionNumber)
                    {
                        regionCreated = true;
                        bool clientCreated = false;
                        // По каждому клиенту в регионе
                        foreach (var client in region.ClientBlocks)
                        {
                            // Если такой клиент уже есть
                            if (client.ClientName == broadcastRecord.ClientName)
                            {
                                clientCreated = true;
                                client.BroadcastRecords.Add(broadcastRecord);
                                region.TotalDuration += broadcastRecord.DurationActual;
                            }
                        }
                        // Если клиента еще не создали
                        if (!clientCreated)
                        {
                            var newClient = new ReportClientBlock()
                            {
                                ClientName = broadcastRecord.ClientName
                            };
                            newClient.BroadcastRecords.Add(broadcastRecord);
                            region.ClientBlocks.Add(newClient);
                            region.TotalDuration += broadcastRecord.DurationActual;
                        }
                    }
                }
                // Если регион еще не создали
                if (!regionCreated)
                {
                    var newRegion = new ReportRegionBlock();
                    newRegion.RegionNumber = broadcastRecord.RegionNumber;
                    regionBlocks.Add(newRegion);
                    // Создаем нового клиента (регион новый, значит, клиента не было)
                    var newClient = new ReportClientBlock()
                    {
                        ClientName = broadcastRecord.ClientName
                    };
                    newClient.BroadcastRecords.Add(broadcastRecord);
                    newRegion.ClientBlocks.Add(newClient);
                    newRegion.TotalDuration += broadcastRecord.DurationActual;
                }
            }
            // Находим блок с партиями (номер региона - не цифры)
            var partyBlock = regionBlocks.Where(x => !Regex.IsMatch(x.RegionNumber, @"^\d+$")).First();
            // Перемещаем блок в конец
            regionBlocks.Remove(partyBlock);
            regionBlocks.Add(partyBlock);
            //
            return regionBlocks;
        }

        public DataTable BuildTotalReport(List<ReportRegionBlock> blocks)
        {
            DataTable dt = new DataTable();
            // Создаем 7 столбцов
            dt.Columns.Add("№ п/п");
            dt.Columns.Add("Ф.И.О. зарегистрированного кандидата");
            dt.Columns.Add("Форма предвыборной агитации");
            dt.Columns.Add("Наименование теле-, радиоматериала");
            dt.Columns.Add("Дата и время выхода в эфир");
            dt.Columns.Add("Объем фактически использованного эфирного времени (час:мин:сек)");
            dt.Columns.Add("Основание предоставления (дата заключения и номер договора)");
            // Счетчик п/п
            int i = 1;
            // Суммарный объем фактически выделенного времени в СМИ
            TimeSpan sumDuration = TimeSpan.Zero;
            // По каждому округу
            foreach (var block in blocks)
            {
                // Если не было вещания фактического, пропускаем.
                if (block.TotalDuration == TimeSpan.Zero) continue;
                //
                dt.Rows.Add();
                // Если номер округа есть, то указываем номер и название
                if (Regex.IsMatch(block.RegionNumber, @"^\d+$"))
                {
                    dt.Rows[dt.Rows.Count - 1][3] = $"{block.RegionCaption} округ № {block.RegionNumber}";
                }
                else
                {
                    dt.Rows[dt.Rows.Count - 1][3] = $"Партии";
                }
                // По каждому клиенту
                foreach (var client in block.ClientBlocks)
                {
                    // Если не было вещания фактического, пропускаем.
                    if (client.TotalDuration == TimeSpan.Zero) continue;
                    //
                    bool firstRow = true;
                    // По каждой записи вещания клиента
                    foreach (var record in client.BroadcastRecords)
                    {
                        // Если не было вещания фактического, пропускаем.
                        if (record.DurationActual == TimeSpan.Zero) continue;
                        //
                        dt.Rows.Add();
                        if (firstRow)
                        {
                            dt.Rows[dt.Rows.Count - 1][0] = i;
                            dt.Rows[dt.Rows.Count - 1][1] = client.ClientName;
                            dt.Rows[dt.Rows.Count - 1][6] = client.ClientContract;
                        }
                        //
                        dt.Rows[dt.Rows.Count - 1][2] = record.BroadcastType;
                        dt.Rows[dt.Rows.Count - 1][3] = record.BroadcastCaption;
                        dt.Rows[dt.Rows.Count - 1][4] = $"{record.Date} {record.Time}";
                        dt.Rows[dt.Rows.Count - 1][5] = record.DurationActual;
                        //
                        firstRow = false;
                    }
                    //
                    if (client.TotalDuration != TimeSpan.Zero)
                    {
                        //
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1][4] = $"Итого";
                        dt.Rows[dt.Rows.Count - 1][5] = client.TotalDuration;
                        //
                        sumDuration += client.TotalDuration;
                        // Увеличения счетчика п/п
                        i++;
                    }
                }
            }
            //
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1][4] = $"Итого";
            dt.Rows[dt.Rows.Count - 1][5] = sumDuration;
            //
            return dt;
        }


        List<BroadcastRecord> BuildBroadcastRecords(DataTable dt)
        {
            List<BroadcastRecord> broadcastRecords = new List<BroadcastRecord>();
            //
            foreach (DataRow row in dt.Rows)
            {
                //
                var record = new BroadcastRecord(
                    row.ItemArray[0].ToString(),
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString(),
                    row.ItemArray[4].ToString(),
                    row.ItemArray[5].ToString(),
                    row.ItemArray[6].ToString(),
                    row.ItemArray[7].ToString(),
                    row.ItemArray[8].ToString(),
                    row.ItemArray[9].ToString());
                //
                broadcastRecords.Add(record);
            }
            //
            return broadcastRecords;
        }

        private void CreateReport(List<ReportRegionBlock> blocks, string templatePath, string resultPath, string mediaresource)
        {
            //
            string fieldMedia = "";
            string fileName = "_";
            switch (mediaresource)
            {
                case "Маяк":
                    //fieldMedia = settings.Наименование_СМИ_Маяк;
                    fileName = "Маяк.docx";
                    break;
                case "Вести ФМ":
                    //fieldMedia = settings.Наименование_СМИ_Вести_ФМ;
                    fileName = "Вести ФМ.docx";
                    break;
                case "Радио России":
                    //fieldMedia = settings.Наименование_СМИ_Радио_России;
                    fileName = "Радио России.docx";
                    break;
                case "Россия 1":
                    //fieldMedia = settings.Наименование_СМИ_Россия_1;
                    fileName = "Россия 1.docx";
                    break;
                case "Россия 24":
                    //fieldMedia = settings.Наименование_СМИ_Россия_24;
                    fileName = "Россия 24.docx";
                    break;
            }
            // Новый 
            var document = new WordDocument(templatePath);
            //// Заполняем поля слияния
            //document.SetMergeFieldText("Наименование_СМИ", $"{fieldMedia}");
            //document.SetMergeFieldText("ИО_Фамилия_предст_СМИ", $"{settings.Кандидаты_ИО_Фамилия_предст_СМИ}");
            //document.SetMergeFieldText("Дата", $"{settings.Кандидаты_Дата}");
            //document.SetMergeFieldText("ИО_Фамилия_члена_изб_ком", $"{settings.Кандидаты_ИО_Фамилия_члена_изб_ком}");
            //
            document.SetBookmarkText($"Учет", "");
            var table = CreateTableReport(blocks);
            document.SetBookmarkTable($"Учет", table);
            // Создает путь для документов, если вдруг каких-то папок нет
            Directory.CreateDirectory(resultPath);
            //
            document.Save(resultPath + $"{fileName}");
            document.Close();
        }


        /// <summary>
        /// Создает таблицу для отчета в ворде
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="parties"></param>
        /// <param name="mediaresource"></param>
        /// <returns></returns>
        Table CreateTableReport(List<ReportRegionBlock> blocks)
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
            //// Now we create a new layout and make it "fixed".
            //TableLayout tl = new TableLayout() { Type = TableLayoutValues.Fixed };
            //tblProp.TableLayout = tl;
            //
            table.Append(tblProp);
            // Заголовки таблицы
            TableRow trHead = new TableRow();
            var tcH4 = new TableCell(CreateParagraph($"Наименование теле -, радиоматериала"));
            tcH4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
            trHead.Append(
                new TableCell(CreateParagraph($"№ п/п")),
                new TableCell(CreateParagraph($"Ф.И.О.\r\n" +
                $"зарегистрированного кандидата,\r\n" +
                $"наименование избирательного объединения, зарегистрировавшего областной список кандидатов")),
                new TableCell(CreateParagraph($"Форма предвыборной агитации ")),
                tcH4,
                new TableCell(CreateParagraph($"Дата и время выхода в эфир")),
                new TableCell(CreateParagraph($"Объем фактически предоставленного эфирного времени (час: мин:сек)")),
                new TableCell(CreateParagraph($"Основание предоставления\r\n(дата заключения\r\nи номер договора)\r\n"))
                );
            // Добавляем заголовок к таблице
            table.Append(trHead);
            // Добавляем строчку с нумерованием столбцов
            TableRow tr = new TableRow();
            TableCell tc1 = new TableCell(CreateParagraph($"1"));
            TableCell tc2 = new TableCell(CreateParagraph($"2"));
            TableCell tc3 = new TableCell(CreateParagraph($"3"));
            TableCell tc4 = new TableCell(CreateParagraph($"4"));
            TableCell tc5 = new TableCell(CreateParagraph($"5"));
            TableCell tc6 = new TableCell(CreateParagraph($"6"));
            TableCell tc7 = new TableCell(CreateParagraph($"7"));
            tr.Append(tc1, tc2, tc3, tc4, tc5, tc6, tc7);
            table.Append(tr);
            // Счетчик п/п
            int i = 1;
            // Суммарный объем фактически выделенного времени в СМИ
            TimeSpan sumDuration = TimeSpan.Zero;
            // По региону
            foreach (var region in blocks)
            {
                // Если не было вещания фактического, пропускаем.
                if (region.TotalDuration == TimeSpan.Zero) continue;
                //
                string regionCaption = "";
                // Если номер округа есть, то указываем номер и название
                if (Regex.IsMatch(region.RegionNumber, @"^\d+$"))
                {
                    regionCaption = $"{region.RegionCaption} округ № {region.RegionNumber}";
                }
                else
                {
                    regionCaption = $"Партии";
                }
                // Добавляем округ одной строкой
                tr = CreateRowMergedCells(regionCaption, 7);
                table.Append((TableRow)tr.CloneNode(true));
                // По клиенту
                foreach (var client in region.ClientBlocks)
                {
                    // Если не было вещания фактического, пропускаем.
                    if (client.TotalDuration == TimeSpan.Zero) continue;
                    //
                    bool firstRow = true;
                    //
                    // По каждой записи вещания клиента
                    foreach (var record in client.BroadcastRecords)
                    {
                        // Если не было вещания фактического, пропускаем.
                        if (record.DurationActual == TimeSpan.Zero) continue;
                        //
                        if (firstRow)
                        {
                            tc1 = new TableCell(CreateParagraph($"{i}"));
                            tc2 = new TableCell(CreateParagraph($"{client.ClientName}"));
                            tc7 = new TableCell(CreateParagraph($"{client.ClientContract}"));
                        }
                        else
                        {
                            tc1 = new TableCell(CreateParagraph($""));
                            tc2 = new TableCell(CreateParagraph($""));
                            tc7 = new TableCell(CreateParagraph($""));
                        }
                        //
                        tc3 = new TableCell(CreateParagraph($"{record.BroadcastType}"));
                        tc4 = new TableCell(CreateParagraph($"{record.BroadcastCaption}"));
                        tc5 = new TableCell(CreateParagraph($"{record.Date} {record.Time}"));
                        tc6 = new TableCell(CreateParagraph($"{record.DurationActual}"));
                        //
                        firstRow = false;
                        //
                        tr = new TableRow();
                        tr.Append((TableCell)tc1.CloneNode(true),
                            (TableCell)tc2.CloneNode(true),
                            (TableCell)tc3.CloneNode(true),
                            (TableCell)tc4.CloneNode(true),
                            (TableCell)tc5.CloneNode(true),
                            (TableCell)tc6.CloneNode(true),
                            (TableCell)tc7.CloneNode(true));
                        table.Append((TableRow)tr.CloneNode(true));
                    }
                    //
                    if (client.TotalDuration != TimeSpan.Zero)
                    {
                        tc1 = new TableCell(CreateParagraph($""));
                        tc2 = new TableCell(CreateParagraph($""));
                        tc3 = new TableCell(CreateParagraph($""));
                        tc4 = new TableCell(CreateParagraph($""));
                        tc5 = new TableCell(CreateParagraph($"Итого"));
                        tc6 = new TableCell(CreateParagraph($"{client.TotalDuration}"));
                        tc7 = new TableCell(CreateParagraph($""));
                        //
                        tr = new TableRow();
                        tr.Append((TableCell)tc1.CloneNode(true),
                            (TableCell)tc2.CloneNode(true),
                            (TableCell)tc3.CloneNode(true),
                            (TableCell)tc4.CloneNode(true),
                            (TableCell)tc5.CloneNode(true),
                            (TableCell)tc6.CloneNode(true),
                            (TableCell)tc7.CloneNode(true));
                        table.Append((TableRow)tr.CloneNode(true));
                        //
                        sumDuration += client.TotalDuration;
                        // Увеличения счетчика п/п
                        i++;
                    }
                }
            }
            //
            tc1 = new TableCell(CreateParagraph($""));
            tc2 = new TableCell(CreateParagraph($""));
            tc3 = new TableCell(CreateParagraph($""));
            tc4 = new TableCell(CreateParagraph($""));
            tc5 = new TableCell(CreateParagraph($"Итого"));
            tc6 = new TableCell(CreateParagraph($"{sumDuration}"));
            tc7 = new TableCell(CreateParagraph($""));
            //
            tr = new TableRow();
            tr.Append((TableCell)tc1.CloneNode(true),
                            (TableCell)tc2.CloneNode(true),
                            (TableCell)tc3.CloneNode(true),
                            (TableCell)tc4.CloneNode(true),
                            (TableCell)tc5.CloneNode(true),
                            (TableCell)tc6.CloneNode(true),
                            (TableCell)tc7.CloneNode(true));
            table.Append((TableRow)tr.CloneNode(true));
            // Возвращаем
            return table;
        }



        /// <summary>
        /// Объединяет указанное количество ячеек в строке вместе, также вставляет туда текст. (в теории, пока нет)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cellsNumber"></param>
        /// <returns></returns>
        private TableRow CreateRowMergedCells(string text, int cellsNumber)
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
        /// Создает новый абзац текста
        /// </summary>
        /// <param name="text"></param>
        /// <param name="style">Для выбора различных дополнений текста типа выравнивания по центру</param>
        /// <returns></returns>
        Paragraph CreateParagraph(string text, string style = "default")
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
    }
}

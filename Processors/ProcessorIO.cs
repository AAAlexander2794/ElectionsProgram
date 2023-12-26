using ElectionsProgram.ViewModel;
using System;
using System.Collections.Generic;
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
    public static class ProcessorIO
    {
        public static void SaveDBToXml(ViewModel.ViewModel electionsBase, string filePath, string fileName)
        {
            // Создаем директорию, если ее не было
            System.IO.Directory.CreateDirectory(filePath);
            //
            XmlSerializer xs = new XmlSerializer(typeof(ViewModel.ViewModel));
            using (TextWriter tw = new StreamWriter($"{filePath}\\{fileName}"))
            {
                xs.Serialize(tw, electionsBase);
            }
        }

        public static ViewModel.ViewModel LoadDBFromXml(string filePath, string fileName)
        {
            ViewModel.ViewModel _base;
            XmlSerializer xs = new XmlSerializer(typeof(ViewModel.ViewModel));
            using (var sr = new StreamReader($"{filePath}\\{fileName}"))
            {
                _base = (ViewModel.ViewModel)xs.Deserialize(sr);
            }
            return _base;
        }
    }
}

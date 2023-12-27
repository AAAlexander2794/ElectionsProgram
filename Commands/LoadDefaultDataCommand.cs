using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    /// <summary>
    /// Команда заполнения базовыми данными
    /// </summary>
    public class LoadDefaultDataCommand : ICommand
    {
        private ViewModels.ViewModel _base;

        public LoadDefaultDataCommand(ViewModels.ViewModel @base)
        {
            _base = @base;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // Создаем базовые медиаресурсы
            MediaresourceView media1 = new MediaresourceView("Россия-1") 
            { 
                Название_полное = "Телеканал \"Российский Информационный Канал \"Россия-24\" (Россия-24)\""
            };
            MediaresourceView media2 = new MediaresourceView("Россия-24") 
            { 
                Название_полное = "Телеканал \"Телеканал \"Россия\" (Россия-1)\"" 
            };
            MediaresourceView media3 = new MediaresourceView("Маяк")
            {
                Название_полное = "Радиоканал \"Маяк\""
            };
            MediaresourceView media4 = new MediaresourceView("Вести ФМ")
            {
                Название_полное = "Радиоканал \"Вести ФМ\""
            };
            MediaresourceView media5 = new MediaresourceView("Радио России") 
            { 
                Название_полное = "Радиоканал \"Радио России\"" 
            };
            _base.Mediaresources.Add(new Mediaresource(media1));
            _base.Mediaresources.Add(new Mediaresource(media2));
            _base.Mediaresources.Add(new Mediaresource(media3));
            _base.Mediaresources.Add(new Mediaresource(media4));
            _base.Mediaresources.Add(new Mediaresource(media5));
            //
            _base.CurrentMediaresource = _base.Mediaresources[0];
            // Создаем дополнительную "партию", обозначающую отсутствие партии
            _base.Parties.Add(new Party(new PartyView("Самовыдвижение")));
        }
    }
}

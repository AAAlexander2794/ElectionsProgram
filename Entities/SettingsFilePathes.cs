using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public class SettingsFilePathes : INotifyPropertyChanged
    {
        /// <summary>
        /// Все текстовые поля
        /// </summary>
        private readonly SettingsFilePathesView _view = new();

        #region Протоколы

        /// <summary>
        /// Путь к папке протоколов жеребьевки.
        /// </summary>
        public string Протоколы_Выходной_каталог { get => _view.Протоколы_Выходной_каталог; 
            set
            {
                _view.Протоколы_Выходной_каталог = value;
                OnPropertyChanged();
            }
        }

        public string Протоколы_Шаблон_Партии { get => _view.Протоколы_Шаблон_Партии;
            set
            {
                _view.Протоколы_Шаблон_Партии = value;
                OnPropertyChanged();
            }
        }

        public string Протоколы_Настройки { get => _view.Протоколы_Настройки;
            set 
            {
                _view.Протоколы_Настройки = value; 
                OnPropertyChanged();
            } 
        }

        #endregion Протоколы

        #region Партии

        public string Партии_Таблица { get => _view.Партии_Таблица;
            set
            {
                _view.Партии_Таблица = value;
                OnPropertyChanged();
            }
        }

        // Талоны бесплатные

        public string Партии_Талоны_Россия_1 { get => _view.Партии_Талоны_Россия_1;
            set
            {
                _view.Партии_Талоны_Россия_1 = value;
                OnPropertyChanged();
            }
        }

        public string Партии_Талоны_Россия_24 { get => _view.Партии_Талоны_Россия_24;
            set
            {
                _view.Партии_Талоны_Россия_24 = value;
                OnPropertyChanged();
            }
        }

        public string Партии_Талоны_Маяк { get => _view.Партии_Талоны_Маяк;
            set
            {
                _view.Партии_Талоны_Маяк = value;
                OnPropertyChanged();
            }
        }

        public string Партии_Талоны_Вести_ФМ { get => _view.Партии_Талоны_Вести_ФМ;
            set
            {
                _view.Партии_Талоны_Вести_ФМ = value;
                OnPropertyChanged();
            } 
        }

        public string Партии_Талоны_Радио_России { get => _view.Партии_Талоны_Радио_России;
            set
            {
                _view.Партии_Талоны_Радио_России = value;
                OnPropertyChanged();
            }
        }

        // Время общего вещания

        public string Партии_Общее_Россия_1 { get => _view.Партии_Общее_Россия_1;
            set
            {
                _view.Партии_Общее_Россия_1 = value; 
                OnPropertyChanged();
            }
        }

        public string Партии_Общее_Россия_24 { get => _view.Партии_Общее_Россия_24;
            set
            {
                _view.Партии_Общее_Россия_24 = value;
                OnPropertyChanged();
            }
        }

        public string Партии_Общее_Маяк { get => _view.Партии_Общее_Маяк;
            set
            {
                _view.Партии_Общее_Маяк = value;
                OnPropertyChanged();
            }
        }

        public string Партии_Общее_Вести_ФМ { get => _view.Партии_Общее_Вести_ФМ;
            set
            {
                _view.Партии_Общее_Вести_ФМ = value;
                OnPropertyChanged();
            }
        }

        public string Партии_Общее_Радио_России { get => _view.Партии_Общее_Радио_России;
            set
            {
                _view.Партии_Общее_Радио_России = value;
                OnPropertyChanged();
            }
        }

        // Шаблоны

        public string Партии_Шаблон_договора_РВ { get; set; }

        public string Партии_Шаблон_договора_ТВ { get; set; }

        #endregion Партии

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

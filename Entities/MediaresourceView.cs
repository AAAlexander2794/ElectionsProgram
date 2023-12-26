using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public class MediaresourceView : INotifyPropertyChanged
    {
        private string _название_полное;
        public string Название_полное { get => _название_полное; 
            set
            {
                _название_полное = value;
                OnPropertyChanged();
            }
        }

        public string Название_краткое { get; set; }

        /// <summary>
        /// Название для работы в таблицах (недопустимо в отчетах).
        /// </summary>
        /// <remarks>
        /// Используется во время жеребьевки.
        /// </remarks>
        public string Название_условное { get; set; }

        /// <summary>
        /// Номер талона.
        /// </summary>
        /// <remarks>
        /// Используется во время жеребьевки.
        /// </remarks>
        public string Номер_талона {  get; set; }

        public string Примечание { get; set; }

        #region Конструкторы

        public MediaresourceView(string название_условное)
        {
            Название_условное = название_условное;
        }

        public MediaresourceView() { }

        #endregion Конструкторы

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

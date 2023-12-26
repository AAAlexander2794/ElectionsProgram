using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Медиаресурс (канал)
    /// </summary>
    public class Mediaresource : INotifyPropertyChanged
    {
        public MediaresourceView View { get; set; }

        #region Конструкторы

        public Mediaresource(MediaresourceView mediaresourceView) 
        {
            View = mediaresourceView;
        }

        public Mediaresource() { }

        #endregion Конструкторы

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

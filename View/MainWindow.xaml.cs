using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ElectionsProgram.ViewModel;

namespace ElectionsProgram.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.ViewModel _electionsBase = new ViewModel.ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _electionsBase;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _electionsBase.Mediaresources[0].View.Название_полное = "Новое полное название";
            MessageBox.Show($"{_electionsBase.Mediaresources.Count}");
        }
    }
}

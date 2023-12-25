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
        ElectionsBase _electionsBase = new ElectionsBase();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _electionsBase;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _electionsBase.Description = "New text";
            MessageBox.Show($"{_electionsBase.Mediaresources.Count}");
        }
    }
}

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
using ElectionsProgram.Builders;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;

namespace ElectionsProgram.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModels.ViewModel _viewModel = new ViewModels.ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Mediaresources.Count > 0)
            {
                _viewModel.Mediaresources[0].View.Название_полное = "Новое полное название";
            }
            _viewModel.Test = ProcessorExcel.ReadExcelSheet(@"Настройки/Партии/Партии.xlsx");
            BuilderParties.BuildParties(_viewModel.Test);
            MessageBox.Show($"{_viewModel.Mediaresources.Count}");
        }
    }
}

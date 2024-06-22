using System;
using System.Windows;
using Microsoft.Win32;
using GoodsViewModel;
using ProductProvider = GoodsLib.Models.Enum.ProductProvider;

namespace GoodsView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainVm _vm = new MainVm();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedProvider == ProductProvider.None)
            {
                MessageBox.Show("Сначала необходимо выбрать поставщика в поле \"Поставщик\"", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog().HasValue)
            {
                try
                {
                    _vm.LoadFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.Products.Count == 0)
            {
                MessageBox.Show("Таблица пустая. Загрузите накладные!", "Информация", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                var dialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*",
                    FileName = "накладная_" + DateTime.Now.ToString("d")
                };
                if (dialog.ShowDialog().HasValue)
                {
                    _vm.SaveProducts(dialog.FileName);
                    MessageBox.Show("Файл сохранен успешно", "Информация", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            _vm.ClearProducts();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            _vm.UpdateProducts();
        }
    }
}
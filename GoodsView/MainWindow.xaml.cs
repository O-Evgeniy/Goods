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
using System.Windows.Navigation;
using System.IO;
using Microsoft.Win32;

using GoodsViewModel;
using System.Collections.ObjectModel;
using GoodsLib.Entity;
using GoodsLib;

namespace GoodsView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM vm = new MainVM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedProvider == ProductProviderEnum.none)
            {
                MessageBox.Show("Сначала необходимо выбрать поставщика в поле \"Поставщик\"", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dialog = new OpenFileDialog();
            dialog.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    vm.LoadFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            dialog.FileName = "накладная_" + DateTime.Now.ToString("d");
            if (vm.Products.Count == 0)
                MessageBox.Show("Таблица пустая. Загрузите накладные!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                if ((bool)dialog.ShowDialog())
                {
                    vm.SaveProducts(dialog.FileName);
                    MessageBox.Show("Файл сохранен успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            vm.ClearProducts();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            vm.Update();
        }
    }
}

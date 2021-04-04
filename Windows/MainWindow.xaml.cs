using FlowersSale.Utils;
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

namespace FlowersSale.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isLogout = false;

        // Флаг выхода из системы
        public bool IsLogout
        {
            get => _isLogout;
            private set => this._isLogout = value;
        }  

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            AuthManager.Context.Logout();
            this.IsLogout = true;
            this.Close();
        }
    }
}

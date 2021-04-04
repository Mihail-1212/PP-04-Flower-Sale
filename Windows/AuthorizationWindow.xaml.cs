using FlowersSale.Content.Auth;
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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            FrameManager.AuthFrame = this.AuthFrame;
            FrameManager.AuthFrame.Navigate(new LoginContent());
            WindowManager.AuthWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Проверка есть ли логин и пароль => переход на главную страницу,
            var user = AuthManager.Context.GetLoadUser();
            if(user != null)
            {
                WindowManager.MainWindow = new MainWindow();
                WindowManager.AuthWindow.Hide();
                WindowManager.MainWindow.Show();
            }
        }
    }
}

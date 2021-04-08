using FlowersSale.Content.Auth;
using FlowersSale.Utils;
using System.Windows;

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

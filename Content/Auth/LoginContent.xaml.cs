using FlowersSale.Utils;
using FlowersSale.Windows;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace FlowersSale.Content.Auth
{
    /// <summary>
    /// Логика взаимодействия для LoginContent.xaml
    /// </summary>
    public partial class LoginContent : UserControl
    {
        public LoginContent()
        {
            InitializeComponent();
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            String login = TextBoxLogin.Text;
            String password = PasswordBoxPassword.Password;
            var errors = new StringBuilder();
            if (String.IsNullOrWhiteSpace(login))
                errors.AppendLine("Вы не ввели логин!");
            if (String.IsNullOrWhiteSpace(password))
                errors.AppendLine("Вы не ввели пароль!");
            if (!Regex.IsMatch(login, Constants.LOGIN_REGEX))
                errors.AppendLine("Ваш пароль не соответствует маске \"xx@xx.xx\"!");
            if(errors.Length != 0)
            {
                MessageShow.Error(errors.ToString()) ;
                return;
            }
            Login(login, password);
        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.AuthFrame.Navigate(new RegistrationContent1());
        }

        private void Login(String login, String password)
        {
            if (!AuthManager.Context.LoginTry(login, password))
            {
                MessageShow.Error("Вы ввели неверный логин или пароль!");
                return;
            }
            else
            {
                // Переход на главное окно
                WindowManager.MainWindow = new MainWindow();
                WindowManager.AuthWindow.Hide();
                WindowManager.MainWindow.Show();
            }
        }
    }
}

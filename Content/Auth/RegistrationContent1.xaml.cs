using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace FlowersSale.Content.Auth
{
    /// <summary>
    /// Логика взаимодействия для RegistrationContent1.xaml
    /// </summary>
    public partial class RegistrationContent1 : UserControl
    {
        private String password;
        private String passwordReset;
        private Users _newUser;

        public RegistrationContent1()
        {
            InitializeComponent();
            this._newUser = new Users();
            RegistrationContext.Context = new RegistrationContext(this, 
                new RegistrationContent2(this._newUser), 
                new RegistrationContent3(this._newUser));
        }

        /// <summary>
        /// Обновление сохраненных паролей
        /// </summary>
        private void RegistrationContent1_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBoxPassword.Password = password;
            PasswordBoxRepeatPassword.Password = passwordReset;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.AuthFrame.GoBack();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            String login = TextBoxLogin.Text;
            password = PasswordBoxPassword.Password;
            passwordReset = PasswordBoxRepeatPassword.Password;
            var errors = new StringBuilder();
            if (String.IsNullOrWhiteSpace(login))
                errors.AppendLine("Вы не ввели логин!");
            if (String.IsNullOrWhiteSpace(password))
                errors.AppendLine("Вы не ввели пароль!");
            if (!Regex.IsMatch(login, Constants.LOGIN_REGEX))
                errors.AppendLine("Ваш пароль не соответствует маске \"xx@xx.xx\"!");
            if (!passwordReset.Equals(password))
                errors.AppendLine("Пароли отличаются!");
            if (!Regex.IsMatch(password, Constants.PASSWORD_REGEX))
                errors.AppendLine("Пароль не соответствует стандартам безопасности!");
            if (errors.Length != 0)
            {
                MessageShow.Error(errors.ToString());
                return;
            }
            this._newUser.login = login;
            this._newUser.password = StringHelper.GetHashString(password);
            FrameManager.AuthFrame.Navigate(RegistrationContext.Context.RegistrationContent2);
        }
    }
}

using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowersSale.Content.Auth
{
    /// <summary>
    /// Логика взаимодействия для RegistrationContent2.xaml
    /// </summary>
    public partial class RegistrationContent2 : UserControl
    {
        private Users _newUser;

        public RegistrationContent2(Users user)
        {
            InitializeComponent();
            this._newUser = user;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.AuthFrame.GoBack();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            String lastName = TextBoxLastName.Text;
            String firstName = TextBoxFirstName.Text;
            String secondName = TextBoxSecondName.Text;
            var errors = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(lastName) && !Regex.IsMatch(lastName, Constants.ALPHABET_REGEX))
                errors.AppendLine("Фамилия должна содержать только буквы русского или латинского алфавита!");
            if (!String.IsNullOrWhiteSpace(firstName) && !Regex.IsMatch(firstName, Constants.ALPHABET_REGEX))
                errors.AppendLine("Имя должно содержать только буквы русского или латинского алфавита!");
            if (!String.IsNullOrWhiteSpace(secondName) && !Regex.IsMatch(secondName, Constants.ALPHABET_REGEX))
                errors.AppendLine("Отчество должно содержать только буквы русского или латинского алфавита!");
            if (errors.Length != 0)
            {
                MessageShow.Error(errors.ToString());
                return;
            }
            this._newUser.last_name = lastName;
            this._newUser.first_name = firstName;
            this._newUser.name = secondName;
            FrameManager.AuthFrame.Navigate(new RegistrationContent3(this._newUser));
        }
    }
}

using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace FlowersSale.Content.Auth
{
    /// <summary>
    /// Логика взаимодействия для RegistrationContent3.xaml
    /// </summary>
    public partial class RegistrationContent3 : UserControl
    {
        private String _gender;
        private Users _newUser;

        public RegistrationContent3(Users user)
        {
            InitializeComponent();
            this._newUser = user;
            // Генерация списка полов
            foreach (Gender gender in Enum.GetValues(typeof(Gender)))
            {
                var radio = new RadioButton()
                {
                    Content = StoreManager.GetGenderDisplay(gender.ToString()),
                    Tag = gender
                };
                radio.Checked += RadioButton_Checked;
                this.StackPanelRadioButtonParent.Children.Add(radio);
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.AuthFrame.GoBack();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            String phone = TextBoxPhone.Text;
            DateTime? date = DatePickerDeteBirth.SelectedDate;

            var errors = new StringBuilder();
            if (!Regex.IsMatch(phone, Constants.PHONE_REGEX))
                errors.AppendLine("Телефон не соответствует маске \"+7ХХХХХХХХХХ\" или \"8ХХХХХХХХХХ\"");
            if (date == null)
                errors.AppendLine("Вы не ввели дату рождения!");
            if (String.IsNullOrEmpty(this._gender))
                errors.AppendLine("Вы не выбрали пол!");
            if (
                this._newUser.id == 0 && 
                FlowersSaleEntities.GetContext()
                .Users.ToList()
                .Where(v => v.login.Equals(this._newUser.login)).Count() != 0
                )
                errors.AppendLine($"Пользователь с логином {this._newUser.login} уже существует в системе!");
            if (errors.Length != 0)
            {
                MessageShow.Error(errors.ToString());
                return;
            }
            if (!MessageShow.Question("Вы точно хотите создать пользователя?"))
                return;
            this._newUser.phone = phone;
            this._newUser.date_of_birth = (DateTime)date;
            this._newUser.gender = this._gender;
            // Сохранение пользователя
            if (this._newUser.id == 0)
            {
                FlowersSaleEntities.GetContext().Users.Add(this._newUser);
            }
            try
            {
                this._newUser.roll = Roll.User.ToString();
                FlowersSaleEntities.GetContext().SaveChanges();
                FlowersSaleEntities.GetContext().Reload();
                MessageShow.Success($"Пользователь \"{this._newUser.login}\" был успешно создан!");
            }
            catch
            {
                MessageShow.Error("Произошла ошибка сохранения!");
            }
            // Переход на другую страницу
            FrameManager.AuthFrame.Navigate(new RegistrationContent4(this._newUser));
        }

        /// <summary>
        /// Выставление гендера
        /// </summary>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _gender = ((Gender)(sender as RadioButton).Tag).ToString();
        }
    }
}

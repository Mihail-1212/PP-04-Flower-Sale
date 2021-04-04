using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FlowersSale.Content.Auth
{
    /// <summary>
    /// Логика взаимодействия для RegistrationContent4.xaml
    /// </summary>
    public partial class RegistrationContent4 : UserControl, INotifyPropertyChanged
    {
        private Users _newUser;
        private Address address;
        private FlowersSaleEntities saleEntities;   // Использование нового контекста для безопасной работы с другой сущностью

        public Address Address
        {
            get => address;
            set
            {
                this.address = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RegistrationContent4(Users user)
        {
            InitializeComponent();
            this._newUser = user;
            this.DataContext = this.Address = new Address();
            this.saleEntities = new FlowersSaleEntities();  
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.saleEntities.Reload();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.AuthFrame.GoFirstPage();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            if (this.Address.id == 0)
            {
                this.saleEntities.Address.Add(Address);
            }
            try
            {
                this.Address.id_user = this._newUser.id;
                this.saleEntities.SaveChanges();
                this.saleEntities.Reload();
                FrameManager.AuthFrame.GoFirstPage();
                MessageShow.Success("Данные успешно сохранены!");
            }
            catch
            {
                MessageShow.Error("Произошла ошибка при сохранении!");
            }
        }

        protected void OnPropertyChanged([CallerMemberName]String prop="")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

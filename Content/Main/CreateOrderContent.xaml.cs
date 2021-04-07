using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace FlowersSale.Content.Main
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderContent.xaml
    /// </summary>
    public partial class CreateOrderContent : UserControl, INotifyPropertyChanged
    {
        public Users User
        {
            get
            {
                return AuthManager.Context.CurrentUser();
            }
        }

        public Address Address
        {
            get
            {
                var address = AuthManager.Context.CurrentUser().Address.First(); 
                if (address == null)
                {
                    address = new Address()
                    {
                        id_user = AuthManager.Context.CurrentUser().id
                    };
                    FlowersSaleEntities.GetContext().Address.Add(address);
                    FlowersSaleEntities.GetContext().SaveChanges();
                    FlowersSaleEntities.GetContext().Reload();
                    OnPropertyChanged("User");
                }

                return address;
            }
        }

        public CreateOrderContent()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String prop="")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ButtonCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();
            if (String.IsNullOrWhiteSpace(User.login) || User.login.Length > 40)
                errors.AppendLine("Вы не ввели email или он больше 40 символов!");
            if (!Regex.IsMatch(User.login, Constants.LOGIN_REGEX))
                errors.AppendLine("Ваш email не соответствует маске \"xx@xx.xx\"!");
            if (String.IsNullOrWhiteSpace(User.last_name) || User.last_name.Length > 20)
                errors.AppendLine("Вы не ввели фамилию или она больше 20 символов!");
            if (String.IsNullOrWhiteSpace(User.first_name) || User.first_name.Length > 20)
                errors.AppendLine("Вы не ввели имя или оно больше 20 символов!");
            if (String.IsNullOrWhiteSpace(User.name) || User.name.Length > 20)
                errors.AppendLine("Вы не ввели отчество или оно больше 20 символов!");
            if (!Regex.IsMatch(User.phone, Constants.PHONE_REGEX))
                errors.AppendLine("Телефон не соответствует маске \"+7ХХХХХХХХХХ\" или \"8ХХХХХХХХХХ\"");

            if (String.IsNullOrWhiteSpace(Address.street) || Address.street.Length > 30)
                errors.AppendLine("Вы не ввели улицу или она больше 30 символов!");
            if (String.IsNullOrWhiteSpace(Address.house) || Address.house.Length > 5)
                errors.AppendLine("Вы не ввели дом или он больше 5 символов!");
            if (String.IsNullOrWhiteSpace(Address.room) || Address.room.Length > 5)
                errors.AppendLine("Вы не ввели квартиру или она больше 5 символов!");

            if (String.IsNullOrWhiteSpace(Address.room) || Address.room.Length > 5)
                errors.AppendLine("Вы не ввели квартиру или она больше 5 символов!");
            if (String.IsNullOrWhiteSpace(Address.room) || Address.room.Length > 5)
                errors.AppendLine("Вы не ввели квартиру или она больше 5 символов!");

            if (!String.IsNullOrEmpty(this.TextBoxFloor.Text) && !Regex.IsMatch(this.TextBoxFloor.Text, Constants.NUMBER_REGEX))
                errors.AppendLine("Подъезд не является числом!");
            if (!String.IsNullOrEmpty(this.TextBoxFloor.Text) && !Regex.IsMatch(this.TextBoxFloor.Text, Constants.NUMBER_REGEX))
                errors.AppendLine("Этаж не является числом!");
            var equalLoginList = FlowersSaleEntities.GetContext().Users.ToList().Where(v => v.login.Equals(User.login)).ToList();
            if (equalLoginList.Count > 0 && equalLoginList.Where(v => v.id != User.id).Count() != 0)
                errors.AppendLine("Пользователь с таким логином уже есть в системе!");
            if (ItemsOrderManager.Context.ItemsOrders.Count == 0)
                errors.AppendLine("Корзина пуста!");

            if (errors.Length > 0)
            {
                MessageShow.Error(errors.ToString());
                return;
            }

            if (MessageShow.Question("Вы уверены, что хотите осуществить заказ и сохранить все данные?"))
            {
                try
                {
                    var order = new Order() { id_user = AuthManager.Context.CurrentUser().id };
                    FlowersSaleEntities.GetContext().Order.AddOrUpdate(order);
                    FlowersSaleEntities.GetContext().SaveChanges();
                    var orderItemList = ItemsOrderManager.Context.ItemsOrders;
                    orderItemList.ForEach(v => v.id_order = order.id);
                    FlowersSaleEntities.GetContext().ItemsOrder.AddRange(orderItemList);
                    FlowersSaleEntities.GetContext().SaveChanges();
                    FlowersSaleEntities.GetContext().Reload();
                    MessageShow.Success($"Заказ на сумму " +
                        $"{String.Format("{0:0.00}", ItemsOrderManager.Context.ItemTotalCost).Replace(',', '.')}" +
                        $" рублей успешно совершен!");
                    ItemsOrderManager.Context.ReloadItemsOrder();
                    ItemsOrderManager.Context.UpdateFull();
                    
                    FrameManager.MainFrame.GoFirstPage();
                }
                catch
                {
                    MessageShow.Error("Произошла ошибка сохранения!");
                }
            }
        }
    }
}

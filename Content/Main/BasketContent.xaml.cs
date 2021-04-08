using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlowersSale.Content.Main
{
    /// <summary>
    /// Логика взаимодействия для BasketContent.xaml
    /// </summary>
    public partial class BasketContent : UserControl, INotifyPropertyChanged
    {
        public List<Items> ItemList
        {
            get
            {
                var items = ItemsOrderManager.Context.ItemsOrders.Select(v => v.Items).ToList();
                return items;
            }
        }

        public BasketContent()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String prop = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("ItemList");
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            var textBox = UIHelper.GetNearestObject<TextBox>(sender as Button, "TextBoxCount");
            var newCount = int.Parse(textBox.Text) - 1;
            textBox.Text = Math.Max(newCount, 0).ToString();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var textBox = UIHelper.GetNearestObject<TextBox>(sender as Button, "TextBoxCount");
            textBox.Text = (int.Parse(textBox.Text) + 1).ToString();
        }

        /// <summary>
        /// Проверка является ли строка в textbox числом
        /// </summary>
        private void TextBoxCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, Constants.NUMBER_REGEX);
        }

        /// <summary>
        /// Обработчик изменения количества товара
        /// </summary>
        private void TextBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var product = textBox.DataContext as Items;

            var number = int.Parse(String.IsNullOrWhiteSpace(textBox.Text) ? "0" : int.Parse(textBox.Text).ToString());
            ItemsOrder itemsOrder = null;
            if (product != null)
                itemsOrder = ItemsOrderManager.Context.ItemsOrders.FirstOrDefault(v => v.Items.id == product.id);

            if (number == 0 && itemsOrder != null)
            {
                // Сделать проверку на необходимость удаления, затем удалить
                if (MessageShow.Question("Вы точно хотите удалить товар из корзины?"))
                {
                    ItemsOrderManager.Context.ItemsOrders.Remove(itemsOrder);
                    ItemsOrderManager.Context.UpdateFull();
                    this.OnPropertyChanged("ItemList");
                    MessageShow.Success("Товар удален из корзины!");
                }
                else
                {
                    number = itemsOrder.items_count;
                    e.Handled = true;
                }
            }

            textBox.Text = number.ToString();
            if (itemsOrder != null)
            {
                itemsOrder.items_count = number;
                ItemsOrderManager.Context.UpdateFull();
            }
        }

        private void TextBoxCount_Loaded(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var itemsOrder = ItemsOrderManager.Context.ItemsOrders.FirstOrDefault(v => v.Items.id == int.Parse(textBox.Tag.ToString()));

            if (itemsOrder != null)
            {
                textBox.Text = itemsOrder.items_count.ToString();
            }
            else
                textBox.Text = "0";
        }
    }
}

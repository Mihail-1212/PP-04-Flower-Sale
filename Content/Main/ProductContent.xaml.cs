using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для ProductContent.xaml
    /// </summary>
    public partial class ProductContent : UserControl, INotifyPropertyChanged
    {
        private Categories categories;

        public List<Items> ItemList
        {
            get
            {
                var items = FlowersSaleEntities.GetContext().Items.ToList();
                items = items.Where(v => v.id_categories == this.categories.id).ToList();
                return items;
            }
        }

        public ProductContent(Categories categories)
        {
            InitializeComponent();
            this.categories = categories;
            this.DataContext = this;
            if (this.categories == null)
                throw new ArgumentNullException();
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

        private void ButtonBasketAdd_Click(object sender, RoutedEventArgs e)
        {
            var buttonBasketAdd = sender as Button;
            var textBox = UIHelper.GetNearestObject<TextBox>(sender as Button, "TextBoxCount");

            buttonBasketAdd.IsEnabled = false;
            var product = buttonBasketAdd.DataContext as Items;
            var productCount = Math.Max(int.Parse(textBox.Text), 1);
            ItemsOrderManager.Context.ItemsOrders.Add(new ItemsOrder() { Items=product, items_count=productCount });
            textBox.Text = productCount.ToString();
            ItemsOrderManager.Context.UpdateFull();
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
            var buttonBasketAdd = UIHelper.GetNearestObject<Button>(sender as DependencyObject, "ButtonBasketAdd");

            var number = int.Parse(String.IsNullOrWhiteSpace(textBox.Text) ? "0" : int.Parse(textBox.Text).ToString());
            ItemsOrder itemsOrder = null;
            if (product != null)
                itemsOrder = ItemsOrderManager.Context.ItemsOrders.FirstOrDefault(v => v.Items.id == product.id);

            if (number == 0 && itemsOrder != null)
            {
                // Сделать проверку на необходимость удаления, затем удалить
                if (MessageShow.Question("Вы точно хотите удалить товар из корзины?"))
                {
                    buttonBasketAdd.IsEnabled = true;
                    ItemsOrderManager.Context.ItemsOrders.Remove(itemsOrder);
                    ItemsOrderManager.Context.UpdateFull();
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
            var buttonBasketAdd = UIHelper.GetNearestObject<Button>(sender as DependencyObject, "ButtonBasketAdd");

            if (itemsOrder != null)
            {
                textBox.Text = itemsOrder.items_count.ToString();
                buttonBasketAdd.IsEnabled = false;
            }
            else
                textBox.Text = "0";
        }
    }
}

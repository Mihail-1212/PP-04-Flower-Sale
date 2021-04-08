using FlowersSale.Models;
using FlowersSale.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlowersSale.Content.Main
{
    /// <summary>
    /// Логика взаимодействия для MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl, INotifyPropertyChanged
    {

        public List<Categories> CategoryList
        {
            get
            {
                var categories = FlowersSaleEntities.GetContext().Categories.ToList();

                return categories;
            }
        }

        public MainContent()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("CategoryList");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String prop="")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Переход на страницу списка товаров
        /// </summary>
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var category = (sender as Border).DataContext as Categories;
            FrameManager.MainFrame.Navigate(new ProductContent(category));
        }
    }
}

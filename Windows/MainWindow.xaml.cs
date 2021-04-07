using FlowersSale.Content.Main;
using FlowersSale.Utils;
using FlowersSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlowersSale.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isLogout = false;

        // Флаг выхода из системы
        public bool IsLogout
        {
            get => _isLogout;
            private set => this._isLogout = value;
        }  

        public MainWindow()
        {
            InitializeComponent();
            FrameManager.MainFrame = this.MainFrame;
            FrameManager.MainFrame.Navigate(new MainContent());
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            AuthManager.Context.Logout();
            this.IsLogout = true;
            this.Close();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            var currentFrame = sender as Frame;
            this.ButtonBack.Visibility = currentFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            if (currentFrame.Content is BasketContent || currentFrame.Content is CreateOrderContent)
                this.ButtonBasket.Visibility = Visibility.Collapsed;
            else
                this.ButtonBasket.Visibility = Visibility.Visible;
            if (currentFrame.Content is BasketContent)
                this.ButtonCreateOrder.Visibility = Visibility.Visible;
            else
                this.ButtonCreateOrder.Visibility = Visibility.Collapsed;
            FlowersSaleEntities.GetContext().Reload();

        }

        private void ButtonLinkVK_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/syrprizko");
        }

        private void ButtonLinkInstagram_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/teddyflowers_perm/");
        }

        private void ButtonLinkTelegram_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://web.telegram.org/");
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }

        private void ButtonBasket_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new BasketContent());
        }

        private void ButtonCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new CreateOrderContent());
        }
    }
}

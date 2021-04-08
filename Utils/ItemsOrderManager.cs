using FlowersSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FlowersSale.Utils
{
    public class ItemsOrderManager : INotifyPropertyChanged
    {
        private static ItemsOrderManager _context;

        public static ItemsOrderManager Context 
        {
            get
            {
                if (_context == null)
                    _context = new ItemsOrderManager();
                return _context;
            }
        }

        public ItemsOrderManager()
        {
            this.ItemsOrders = new List<ItemsOrder>();
        }

        private List<ItemsOrder> itemsOrders;

        public List<ItemsOrder> ItemsOrders
        {
            get => this.itemsOrders;
            private set
            {
                this.itemsOrders = value;
                OnPropertyChanged();
            }
        }

        public void ReloadItemsOrder()
        {
            this.ItemsOrders = new List<ItemsOrder>();
        }

        public void UpdateFull()
        {
            OnPropertyChanged("ItemsOrders");
            OnPropertyChanged("ItemTotalCost");
        }

        /// <summary>
        /// Полная цена
        /// </summary>
        public decimal ItemCost
        {
            get
            {
                return ItemsOrders.Sum(v => v.items_count * v.Items.price);
            }
        }

        /// <summary>
        /// Цена скидки
        /// </summary>
        public decimal ItemDiscountCost
        {
            get
            {
                DayOfWeek day = DateTime.Now.DayOfWeek;
                decimal discountNum = 0;
                if (AuthManager.Context.CurrentUser().gender == Gender.Man.ToString() && day == DayOfWeek.Friday)
                    discountNum = 0.15M;
                if (AuthManager.Context.CurrentUser().gender == Gender.Woman.ToString() && day == DayOfWeek.Saturday)
                    discountNum = 0.2M;
                return ItemCost * discountNum;
            }
        }

        /// <summary>
        /// Итог
        /// </summary>
        public decimal ItemTotalCost
        {
            get
            {
                return this.ItemCost - this.ItemDiscountCost;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]String prop="")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

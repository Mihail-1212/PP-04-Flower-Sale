using FlowersSale.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlowersSale.Utils
{
    public static class WindowManager
    {
        private static MainWindow _mainWindow;

        public static MainWindow MainWindow 
        {
            get
            {
                return _mainWindow;
            }
            set
            {
                _mainWindow = value;
                _mainWindow.Closed += (s, e) =>
                {
                    if (_mainWindow.IsLogout)
                        AuthWindow.Show();
                    else
                        AuthWindow.Close();
                };
            }
        }

        public static Window AuthWindow;
    }
}

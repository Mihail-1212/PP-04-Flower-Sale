using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace FlowersSale.Utils
{
    public class StoreManager
    {
        public static object GetParameter(String key)
        {
            return Properties.Settings.Default[key];
        }

        public static void SaveParameter(String key, object value)
        {
            Properties.Settings.Default[key] = value;
            Properties.Settings.Default.Save();
        }

        public static void DeleteParameter(String key)
        {
            Properties.Settings.Default[key] = null;
            Properties.Settings.Default.Save();
        }

        public static String GetGenderDisplay(String gender)
        {
            var @switch = new Dictionary<String, String>()
            {
                { Gender.Man.ToString(), "Мужской" },
                { Gender.Woman.ToString(), "Женский" }
            };
            if (!@switch.ContainsKey(gender))
                throw new NotImplementedException($"Вы не реализовали перевод из ключа \"{gender}\"");
            return @switch[gender];
        }
    }

    /// <summary>
    /// Класс расширения для Frame
    /// </summary>
    static class NewFrameClass
    {

        /// <summary>
        /// Переход на первую страницу фрейма
        /// </summary>
        /// <param name="frame">Фрейм</param>
        public static void GoFirstPage(this Frame frame)
        {
            while (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}

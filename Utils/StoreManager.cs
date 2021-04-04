using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

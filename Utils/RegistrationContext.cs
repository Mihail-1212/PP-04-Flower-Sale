using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FlowersSale.Utils
{
    class RegistrationContext
    {
        private static RegistrationContext _context;

        public static RegistrationContext Context
        {
            get => _context;
            set => _context = value;
        }

        private UserControl registrationContent1;
        private UserControl registrationContent2;
        private UserControl registrationContent3;

        public UserControl RegistrationContent1
        {
            get => registrationContent1;
            set => registrationContent1 = value;
        }

        public UserControl RegistrationContent2
        {
            get => registrationContent2;
            set => registrationContent2 = value;
        }

        public UserControl RegistrationContent3
        {
            get => registrationContent3;
            set => registrationContent3 = value;
        }

        public RegistrationContext(UserControl registrationContent1, 
            UserControl registrationContent2, UserControl registrationContent3)
        {
            RegistrationContent1 = registrationContent1;
            RegistrationContent2 = registrationContent2;
            RegistrationContent3 = registrationContent3;
        }

        
    }
}

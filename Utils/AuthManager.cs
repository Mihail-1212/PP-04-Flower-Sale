using FlowersSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowersSale.Utils
{
    public class AuthManager
    {
        private static AuthManager _context;

        public static AuthManager Context
        {
            get
            {
                if (_context == null)
                    _context = new AuthManager();
                return _context;
            }
        }

        private Users _currentUser;


        public bool LoginTry(String login, String password)
        {
            var user = GetUser(login, password);
            if (user == null)
                return false;
            this._currentUser = user;
            StoreManager.SaveParameter(Constants.LOGIN_KEY, login);
            StoreManager.SaveParameter(Constants.PASSWORD_KEY, password);
            return true;
        }

        private Users GetUser(String login, String password)
        {
            var user = FlowersSaleEntities.GetContext().Users.ToList()
                .FirstOrDefault(v => v.login.Equals(login) && v.password.Equals(StringHelper.GetHashString(password)));
            return user;
        }

        public Users CurrentUser()
        {
            return _currentUser;
        }

        public Users GetLoadUser()
        {
            var user = GetUser(StoreManager.GetParameter(Constants.LOGIN_KEY).ToString(), StoreManager.GetParameter(Constants.PASSWORD_KEY).ToString());
            this._currentUser = user;
            return user;
        }

        public void Logout()
        {
            this._currentUser = null;
            StoreManager.DeleteParameter(Constants.LOGIN_KEY);
            StoreManager.DeleteParameter(Constants.PASSWORD_KEY);
        }
    }
}

using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMWPFDesktopUI.ViewModels
{
    public class LoginViewModel :Screen
    {
        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set { _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool CanLogin
        {
            get { return !string.IsNullOrWhiteSpace(Username) 
                    && !string.IsNullOrWhiteSpace(Password); }
        }

        public void Login()
        {
            Console.WriteLine();
        }
    }
}

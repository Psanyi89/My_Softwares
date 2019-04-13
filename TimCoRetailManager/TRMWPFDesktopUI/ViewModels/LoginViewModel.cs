using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using TRMWPFDesktopUI.Helpers;

namespace TRMWPFDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _apiHelper;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool CanLogin => !string.IsNullOrWhiteSpace(Username)
                    && !string.IsNullOrWhiteSpace(Password);

        public async Task Login()
        {
            try
            {
                Models.AuthenticatedUser result = await _apiHelper.Authenticate(Username, Password);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}

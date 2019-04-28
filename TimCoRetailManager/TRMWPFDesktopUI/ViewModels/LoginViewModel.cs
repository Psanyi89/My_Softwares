using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Model;
using TRMWPFDesktopUI.EventModels;
using TRMWPFDesktopUI.Helpers;

namespace TRMWPFDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
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


        public bool IsErrorVisible => !string.IsNullOrWhiteSpace(ErrorMessage);

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }


        public bool CanLogin => !string.IsNullOrWhiteSpace(Username)
                    && !string.IsNullOrWhiteSpace(Password);

        public async Task Login()
        {
            try
            {
                ErrorMessage = string.Empty;
                AuthenticatedUser result = await _apiHelper.Authenticate(Username, Password);
                // Capture more information about the user
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                _events.PublishOnUIThread(new LogOnEvent());

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }
    }
}

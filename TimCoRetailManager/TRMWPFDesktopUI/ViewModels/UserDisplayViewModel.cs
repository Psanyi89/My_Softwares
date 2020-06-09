using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Model;

namespace TRMWPFDesktopUI.ViewModels
{
   public class UserDisplayViewModel :Screen
    {
        private readonly StatusInfoViewModel _statusInfo;
        private readonly IWindowManager _window;
        private readonly IUserEndpoint _userEndpoint;

        private BindingList<UserModel> _users;
        public BindingList<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        public UserDisplayViewModel (StatusInfoViewModel statusInfo, IWindowManager window,
            IUserEndpoint userEndpoint)
            {
            _statusInfo = statusInfo;
            _window = window;
            _userEndpoint = userEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {


            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _statusInfo.UpdateMessage("Unauthorized access", $"{ex.Message}\n you dont have permission to interact with the sales form ");
                }
                else
                {
                    _statusInfo.UpdateMessage("Fatal Exception", ex.Message);
                }
                _window.ShowDialog(_statusInfo, null, settings);
                TryClose();
            }
        }

        private async Task LoadUsers()
        {
            var userList = await _userEndpoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }
    }
}

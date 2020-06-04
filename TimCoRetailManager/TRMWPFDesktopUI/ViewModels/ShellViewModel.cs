using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Model;
using TRMWPFDesktopUI.EventModels;

namespace TRMWPFDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private string _title="ShellView";

        public string Title
        {
            get { return _title; }
            set { _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private readonly ILoggedInUserModel _user;
        private readonly IAPIHelper _aPIHelper;

        public ShellViewModel(IEventAggregator events
            ,SalesViewModel salesVM, ILoggedInUserModel user,
            IAPIHelper aPIHelper)
        {
            _events = events;
            _events.Subscribe(this);
            _salesVM = salesVM;
            _user = user;
            _aPIHelper = aPIHelper;
            AddVersionNumber();
            ActivateItem (IoC.Get<LoginViewModel>());
        }
        private void AddVersionNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Title = $"{Title} v.{versionInfo.FileVersion}";
        }
        public void Handle(LogOnEvent message)
        {
          ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void ExitApplication()
        {
            this.TryClose();
        }

        public void LogOut()
        {
            _user.ResetUserModel();
            _aPIHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_user.Token);
            }
        }
    }
}

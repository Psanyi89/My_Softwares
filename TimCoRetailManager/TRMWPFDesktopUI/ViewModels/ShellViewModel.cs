using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public ShellViewModel(IEventAggregator events
            ,SalesViewModel salesVM)
        {
            _events = events;
            _events.Subscribe(this);
            _salesVM = salesVM;         
        
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
        }
    }
}

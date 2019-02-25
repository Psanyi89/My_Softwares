using Caliburn.Micro;
using ConsignmentShopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopUI.ViewModels
{
   public class ConfirmationDialogViewModel :Screen
    {
        private string _title;
        private string _message;
        private DialogResult _myDialogResult;
        private DialogType _myDialogType;
        private string _buttonOne;
        private string _buttonTwo;

        public string ButtonTwo
        {
            get { return _buttonTwo; }
            set
            {
                _buttonTwo = value;
                NotifyOfPropertyChange(() => ButtonTwo);
            }
        }

        public string ButtonOne
        {
            get { return _buttonOne; }
            set
            {
                _buttonOne = value;
                NotifyOfPropertyChange(() => ButtonOne);
            }
        }

        public DialogType MyDialogType

        {
            get { return _myDialogType; }
            set
            {
                _myDialogType = value;
                NotifyOfPropertyChange(() => MyDialogType);
            }
        }

        public DialogResult MyDialogResult
        {
            get { return _myDialogResult; }
            set
            {
                _myDialogResult = value;
                NotifyOfPropertyChange(() => MyDialogResult);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public void Yes()
        {
            MyDialogResult = DialogResult.Yes;
            TryClose();
        }

        public void No()
        {
            MyDialogResult = DialogResult.No;
            TryClose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public string Title { set; get; } = "MyToDo";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
        private string userName;

        private string account;

        private string passWord;
        public string UserName { set { userName = value; RaisePropertyChanged(); } get { return userName; } }
        public string Account { set { account = value; RaisePropertyChanged(); } get { return account; } }
        public string PassWord { set { passWord = value; RaisePropertyChanged(); } get { return passWord; } }

        public DelegateCommand<string> ExecuteCommand { set; get; }
        public LoginViewModel()
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":
                    Login();break;               
                case "LoginOut":
                    LoginOut();break;
                default:
                    break;
            }
        }

        private void LoginOut()
        {
            throw new NotImplementedException();
        }

        private void Login()
        {
            throw new NotImplementedException();
        }
    }
}

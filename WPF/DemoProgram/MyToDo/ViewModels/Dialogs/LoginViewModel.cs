using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Services.Dialogs;
using MyToDo.Service;

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
            LoginOut();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
        private string userName;

        private string account;

        private string passWord;
        private readonly ILoginService service;

        public string UserName { set { userName = value; RaisePropertyChanged(); } get { return userName; } }
        public string Account { set { account = value; RaisePropertyChanged(); } get { return account; } }
        public string PassWord { set { passWord = value; RaisePropertyChanged(); } get { return passWord; } }

        public DelegateCommand<string> ExecuteCommand { set; get; }
        public LoginViewModel(ILoginService service)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.service = service;
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
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));

        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) ||string.IsNullOrWhiteSpace(PassWord))
            {
                return;
            }
            var loginResult =await service.LoginAsync(new Shared.Dtos.UserDto()
            {
                Account=Account,
                PassWord=PassWord
            });
            if (loginResult.Status)
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            //登录失败提示

        }
    }
}

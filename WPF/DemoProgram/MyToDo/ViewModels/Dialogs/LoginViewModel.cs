using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Services.Dialogs;
using MyToDo.Service;
using MyToDo.Shared.Dtos;

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
            RegisterDto = new RegisterDto();
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":
                    Login();break;           
                case "LoginOut":
                    LoginOut();break;              
                        case "Go":
                    SelectedIndex=1; break;        
                case "Register":
                    Register(); break;              
                        case "Back":
                    SelectedIndex=0;break;
                default:
                    break;
            }
        }
        private RegisterDto registerDto;
        public RegisterDto RegisterDto { get => registerDto; set { registerDto = value; RaisePropertyChanged(); } }

        private async void Register()
        {
            if (string.IsNullOrEmpty(RegisterDto.Account)||string.IsNullOrEmpty(RegisterDto.UserName) || string.IsNullOrEmpty(RegisterDto.PassWord) || string.IsNullOrEmpty(RegisterDto.NewPassWord))
            {
                return;//输入内容不合法
            }
            if (RegisterDto.PassWord!= RegisterDto.NewPassWord)
            {
                return;//两次输入的密码不一致
            }
           var registerResult= await service.Register(new Shared.Dtos.UserDto() {
           Account= RegisterDto.Account,
           UserName = RegisterDto.UserName,
           PassWord = RegisterDto.PassWord
            });
            if (registerResult.Status && registerResult!=null)
            {
                selectedIndex = 0;
            }
            //注册失败

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
        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

    }
}

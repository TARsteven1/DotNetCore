using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Events;
using Prism.Commands;
using Prism.Services.Dialogs;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Extensions;
using MyToDo.Common.User;

namespace MyToDo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        //声明事件聚合器
        private readonly IEventAggregator eventAggregator;
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
        public LoginViewModel(ILoginService service, IEventAggregator eventAggregator)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.service = service;
            this.eventAggregator = eventAggregator;
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
                eventAggregator.SendMessage("输入内容不合法!","Login");
                return;//输入内容不合法
            }
            if (RegisterDto.PassWord!= RegisterDto.NewPassWord)
            {
                eventAggregator.SendMessage("两次输入的密码不一致!", "Login");
                return;//两次输入的密码不一致
            }
            //判断用户名或账户是否已存在
            //if (RegisterDto.Account)
            //{

            //}
           var registerResult= await service.Register(new Shared.Dtos.UserDto() {
           Account= RegisterDto.Account,
           UserName = RegisterDto.UserName,
           PassWord = RegisterDto.PassWord
            });
            if (registerResult.Status && registerResult!=null)
            {
                SelectedIndex = 0;
                eventAggregator.SendMessage("注册成功!", "Login");
                Account = RegisterDto.Account;
                return;
            }
            //注册失败
            eventAggregator.SendMessage(registerResult.Message);
        }

        private void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
            eventAggregator.SendMessage("已退出!", "Login");

        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) ||string.IsNullOrWhiteSpace(PassWord))
            {
                eventAggregator.SendMessage("请输入正确的账号或密码!", "Login");
                return;
            }
            var loginResult =await service.LoginAsync(new Shared.Dtos.UserDto()
            {
                Account=Account,
                PassWord=PassWord,
                UserName=UserName
            });
            if (loginResult!=null&&loginResult.Status)
            {
                AppSession.UserName = loginResult.Result.UserName;
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                eventAggregator.SendMessage("登陆成功!", "Login");
                return;
            }
            //登录失败提示
            eventAggregator.SendMessage(loginResult.Message,"Login");

        }
        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

    }
}

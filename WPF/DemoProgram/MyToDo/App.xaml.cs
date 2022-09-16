using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using MyToDo.Common.Interfaces;
using MyToDo.Service;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Dialogs;
using MyToDo.ViewModels.Messages;
using MyToDo.Views;
using MyToDo.Views.Dialogs;
using MyToDo.Views.Messages;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            //使用官方提供的容器拿到主窗口并返回
            return Container.Resolve<MainView>();
        }
        public static void LoginOut(IContainerProvider Container)
        {
            Current.MainWindow.Hide();
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback => {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
            Current.MainWindow.Show();

            });
        }
        protected override void OnInitialized()
        {

          var dialog=  Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView",callback=> {
                if (callback.Result!=ButtonResult.OK)
                {
                    //Application.Current.Shutdown();
                    Environment.Exit(0);
                    return;
                }
                var service = App.Current.MainWindow.DataContext as IConfigureService;
                if (service != null)
                {
                    service.Configure();
                }
                base.OnInitialized();
            });
           

        }
        //使用容器的类型注册器来注册我们要使用的页面，依赖或者服务
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();

            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView>();
            containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>();

            //注册HttpRestClient,注意填写正确访问地址
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey:"webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:13402/", serviceKey: "webUrl");
            //注册服务
            containerRegistry.Register<IToDoService,ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.Register<ILoginService, LoginService>();

            containerRegistry.RegisterForNavigation<AddToDoView, AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoView, AddMemoViewModel>();

            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();

        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using WpfPrism.UserControls;
using System.Windows;
using WpfPrism;
using ModuleA.UserControls;
using Prism.Events;

namespace WpfPrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        private IRegionManager regionManager;

        public DelegateCommand<string> OpenCommand { get; private set; }
        public DelegateCommand VisitCommand { get; private set; }
        public DelegateCommand ShowCommand { get; private set; }

        private void Open(string obj)
        {
            //先通过IRegionManager接口获取当前全局定义的可用区域
            //往此动态设置内容
            //设置内容的方式时通过依赖注入的形式
            regionManager.Regions["ContentRegion"].RequestNavigate(obj);
        }
        public MainWindowViewModel(IRegionManager regionmanager, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionmanager;

            #region RegionPart
            OpenCommand = new DelegateCommand<string>(Open);
            //regionManager.Regions["ContentRegion2"].RequestNavigate("RegionControl");
            regionManager.RegisterViewWithRegion("ContentRegion2", typeof(RegionControl));
            //将用户控件类型的视图RegionControl注册到区域区域中
            //regionmanager.RegisterViewWithRegion("ContentRegion", typeof(RegionControl));
            #endregion

            #region Module
            VisitCommand = new DelegateCommand(GetRegion);
            ShowCommand = new DelegateCommand(ShowRegion);
            #endregion

            #region MVVM
            MVVMValue = "hello";
            SetValCommand = new DelegateCommand(() => { MVVMValue = "\r\nPrism"; });
            AddValCommand = new DelegateCommand(() => { MVVMValue += ":CompositeCommand"; });
            //实例化复合命令,并注册单挑命令到复合命令中
            SetValCommands = new CompositeCommand();
            SetValCommands.RegisterCommand(SetValCommand);
            SetValCommands.RegisterCommand(AddValCommand);

            //事件订阅与发布
            SubscribeCommand = new DelegateCommand(() =>
            {
                //订阅消息 ,重载过滤器,如果是指定字符串则过滤,多用于指定目标发送信息
                eventAggregator.GetEvent<MessageEvent>().Subscribe(OnMessageReceived, ThreadOption.PublisherThread, false, msg =>
                {
                    if (msg.Equals("Hello")) return false;
                    else return true;
                }
                );
            });
            SendCommand = new DelegateCommand(() =>
            {
                //订阅消息
                eventAggregator.GetEvent<MessageEvent>().Publish("发布消息: ");
                //取消订阅(需要单独一条命令承载,故此处注释)
                //eventAggregator.GetEvent<MessageEvent>().Unsubscribe(OnMessageReceived);
            });
            #endregion

            #region Navigation
            OpenACommand = new DelegateCommand(OpenA);
            OpenBCommand = new DelegateCommand(OpenB);
            GoForwordCommand = new DelegateCommand(GoForword);
            GoBackCommand = new DelegateCommand(GoBack);

            #endregion
        }
        #region Region
        private void ShowRegion(/*IRegionManager regionmanager*/)
        {
            //通过name将ContentRegion的区域显示到名为Ctr的ContentControl
            RegionManager.SetRegionName(MainWindow.ctr, "ContentRegion2");
        }

        private void GetRegion()
        {
            //这两句功能类似,都是打开导航,另外词句意图通过获取ContentRegion的view,将其赋值给ContentRegion2
            //regionManager.RequestNavigate("ContentRegion2", "region.Name");
            regionManager.Regions["ContentRegion2"].RequestNavigate(regionManager.Regions["ContentRegion"].Name);
            //上句,有问题不生效

        }
        #endregion

        #region MVVM
        //绑定属性
        private string mvvmvalue;

        public string MVVMValue
        {
            get { return mvvmvalue; }
            set { mvvmvalue = value; RaisePropertyChanged(); }
        }
        //声明命令
        public DelegateCommand SetValCommand { set; get; }
        public DelegateCommand AddValCommand { set; get; }
        //复合命令:绑定多条命令(Prism独有)
        public CompositeCommand SetValCommands { set; get; }


        private readonly IEventAggregator eventAggregator;
        public DelegateCommand SubscribeCommand { set; get; }
        public DelegateCommand SendCommand { set; get; }
        //事件动作
        public void OnMessageReceived(string message)
        {
            MVVMValue += message + "\r\n";
        }
        #endregion

        #region Navigation
        public DelegateCommand OpenACommand { set; get; }
        public DelegateCommand OpenBCommand { set; get; }
        public DelegateCommand GoForwordCommand { set; get; }
        public DelegateCommand GoBackCommand { set; get; }

        private void OpenA(/*IRegionManager regionmanager*/)
        {
            //基础的打开导航语句
            //regionManager.RequestNavigate("NavigationContentRegion", "ModuleViewA");
            //在导航完成后添加导航日志
            regionManager.RequestNavigate("NavigationContentRegion", "ModuleViewA",arg=> {
                journal = arg.Context.NavigationService.Journal;
            }
            );
        }
        private void OpenB(/*IRegionManager regionmanager*/)
        {
            //定义传参(方式一,类http请求)
            regionManager.RequestNavigate("NavigationContentRegion", $"ViewB?value=Hi", arg => {
                journal = arg.Context.NavigationService.Journal;
            });
            //定义传参(方式二,传统方式,说明传参的功能基于键值对)
            //NavigationParameters pairs = new NavigationParameters();
            //pairs.Add("value", "Hello");
            //regionManager.RequestNavigate("NavigationContentRegion", "ViewB", pairs);

        }
        private void GoForword(/*IRegionManager regionmanager*/)
        {
            if (journal.CanGoForward) journal.GoForward();
        }
        private void GoBack(/*IRegionManager regionmanager*/)
        {
            if (journal.CanGoBack) journal.GoBack();
        }
        //定义导航日志
        IRegionNavigationJournal journal;
        #endregion
    }
    //定义消息类型,注册此消息传递字符串信息
    public class MessageEvent : PubSubEvent<string>
    {

    }
}

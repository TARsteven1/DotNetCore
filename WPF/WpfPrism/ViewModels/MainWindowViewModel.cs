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
  public  class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        private  IRegionManager regionManager;
        public IRegion region;

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
        public MainWindowViewModel(IRegionManager regionmanager ,IEventAggregator eventAggregator)
        {
            OpenCommand = new DelegateCommand<string>(Open);
            VisitCommand = new DelegateCommand(GetRegion);
            ShowCommand = new DelegateCommand( ShowRegion );
            this.regionManager = regionmanager;
            this.eventAggregator = eventAggregator;

            //regionManager.Regions["ContentRegion2"].RequestNavigate("RegionControl");

            regionManager.RegisterViewWithRegion("ContentRegion2", typeof(RegionControl));

            #region RegionPart
            //将用户控件类型的视图RegionControl注册到区域区域中
            //regionmanager.RegisterViewWithRegion("ContentRegion", typeof(RegionControl));

            #endregion

            #region MVVM
            MVVMValue = "hello";
            SetValCommand = new DelegateCommand(()=>{MVVMValue="\r\nPrism"; });
            AddValCommand = new DelegateCommand(() => { MVVMValue += ":CompositeCommand"; });
            //实例化复合命令,并注册单挑命令到复合命令中
            SetValCommands = new CompositeCommand();
            SetValCommands.RegisterCommand(SetValCommand);
            SetValCommands.RegisterCommand(AddValCommand);

            //事件订阅与发布
            SubscribeCommand = new DelegateCommand(()=>
            {
                //订阅消息 ,重载过滤器,如果是指定字符串则过滤,多用于指定目标发送信息
                eventAggregator.GetEvent<MessageEvent>().Subscribe(OnMessageReceived,ThreadOption.PublisherThread,false,msg=> {
                    if (msg.Equals("Hello") )return false;
                    else return true; }
                );
            });
            SendCommand = new DelegateCommand(()=>
            {
                //订阅消息
                eventAggregator.GetEvent<MessageEvent>().Publish("发布消息: ");
                //取消订阅(需要单独一条命令承载,故此处注释)
                //eventAggregator.GetEvent<MessageEvent>().Unsubscribe(OnMessageReceived);
            });
            #endregion
        }

        //对区域的访问
        public IRegion GetRegion(IRegionManager regionmanager, string name)
        {
            region = regionmanager.Regions[/*"ContentRegion"*/name];
            return region;
        }

        private void ShowRegion(/*IRegionManager regionmanager*/)
        {

            //通过name将ContentRegion的区域显示到名为Ctr的ContentControl
            RegionManager.SetRegionName(MainWindow.ctr, "ContentRegion2");
        }

        private void GetRegion()
        {
            var region = GetRegion(regionManager, "ContentRegion");
            //RegionManager.SetRegionContext(MainWindow.ctr, region);
            //regionManager.AddToRegion("ContentRegion", MainWindow.ctr);

            regionManager.Regions["ContentRegion2"].RequestNavigate(region.Name);

        }
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
        public CompositeCommand SetValCommands{ set; get; }

        #endregion

        private readonly IEventAggregator eventAggregator;
        public DelegateCommand SubscribeCommand { set; get; }
        public DelegateCommand SendCommand { set; get; }
        //事件动作
        public void OnMessageReceived(string message)
        {
            MVVMValue += message + "\r\n";
        }

    }
    //定义消息类型,注册此消息传递字符串信息
    public class MessageEvent : PubSubEvent<string>
    {

    }
}

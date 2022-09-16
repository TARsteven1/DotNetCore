using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Common.Interfaces;
using System.Windows.Controls;
using MyToDo.Views;
using MyToDo.Common.User;
using Prism.Ioc;

namespace MyToDo.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        //public static ListBox listBox;
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public MainViewModel(IRegionManager regionManager , IContainerProvider Container)
        {
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
            container = Container;
            GoBackCommand = new DelegateCommand(() => { if (Journal!=null&&Journal.CanGoBack) Journal.GoBack(); MainView.listBox.SelectedItem = null; });
            GoForwardCommand = new DelegateCommand(() => { if (Journal != null && Journal.CanGoForward) Journal.GoForward(); MainView.listBox.SelectedItem = null; });
            //Navigate(MenuBars[0]);
            LoginOutCommand = new DelegateCommand(()=>
            {
                //注销操作
                App.LoginOut(Container);
            });
        }

        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }

        private readonly IRegionManager regionManager;
        private readonly IContainerProvider container;
        private IRegionNavigationJournal Journal;

        public DelegateCommand<MenuBar> NavigateCommand { private set; get; }
        public DelegateCommand GoBackCommand { private set; get; }
        public DelegateCommand GoForwardCommand { private set; get; }
        public DelegateCommand LoginOutCommand { private set; get; }
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace)) return;

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                Journal = back.Context.NavigationService.Journal;
            });
        }
        //配置首页初始化参数
        public void Configure()
        {
            CreateMenuBar();
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
            UserName = AppSession.UserName;
            MenuBars = new ObservableCollection<MenuBar>();


        }
        //private int navigateNum;

        //public int NavigateNum
        //{
        //    get {
        //        //我想访问指定区域,根据区域的view名称判断索引值,失败:我不会访问区域
        //        var ViewName = regionManager.Regions[PrismManager.MainViewRegionName].ActiveViews.ToString();

        //        switch (ViewName)
        //        {
        //            case "IndexView":
        //                navigateNum= 0;break;                
        //            case "ToDoView":
        //                navigateNum= 1;break;                  
        //            case "MemoView":
        //                navigateNum= 2;break;                 
        //            case "SettingView":
        //                navigateNum= 3;break;
        //            default:
        //                break;
        //        }
        //        return navigateNum; }
        //    set { navigateNum = value;  RaisePropertyChanged(); }
        //}

    }
}

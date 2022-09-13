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

namespace MyToDo.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        public MainViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;

            GoBackCommand = new DelegateCommand(() => { if (Journal!=null&&Journal.CanGoBack) Journal.GoBack(); });
            GoForwardCommand = new DelegateCommand(() => { if (Journal != null && Journal.CanGoForward) Journal.GoForward(); });
            //Navigate(MenuBars[0]);
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
        private IRegionNavigationJournal Journal;

        public DelegateCommand<MenuBar> NavigateCommand { private set; get; }
        public DelegateCommand GoBackCommand { private set; get; }
        public DelegateCommand GoForwardCommand { private set; get; }
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
        }
    }
}

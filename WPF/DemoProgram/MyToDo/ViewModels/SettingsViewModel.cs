using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using MyToDo.Common.Models;
using System.Collections.ObjectModel;
using MyToDo.Extensions;
using MyToDo.Views;

namespace MyToDo.ViewModels
{
    public class SettingsViewModel:BindableBase
    {
        public SettingsViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
            CreateMenuBar();

        }
        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "Infomation", Title = "关于更多", NameSpace = "AboutView" });
        }
        public DelegateCommand<MenuBar> NavigateCommand { private set; get; }
        private IRegionNavigationJournal Journal;
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace)) return;

            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                Journal = back.Context.NavigationService.Journal;
            });
        }
    }
}

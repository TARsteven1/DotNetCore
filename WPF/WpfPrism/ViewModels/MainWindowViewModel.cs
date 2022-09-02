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

namespace WpfPrism.ViewModels
{
  public  class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        private readonly IRegionManager regionManager;
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
        //public MainWindowViewModel() { }
        public MainWindowViewModel(IRegionManager regionmanager)
        {
            OpenCommand = new DelegateCommand<string>(Open);
            VisitCommand = new DelegateCommand(GetRegion);
            ShowCommand = new DelegateCommand(()=>{  regionmanager.RegisterViewWithRegion("ContentRegion2", typeof(RegionControl));

            //通过name将ContentRegion的区域显示到名为Ctr的ContentControl
            RegionManager.SetRegionName(MainWindow.ctr, "ContentRegion2");});
            this.regionManager = regionmanager;

            #region RegionPart
            //将用户控件类型的视图RegionControl注册到区域区域中
            //regionmanager.RegisterViewWithRegion("ContentRegion", typeof(RegionControl));

            #endregion
        }

        //对区域的访问
        public IRegion GetRegion(IRegionManager regionmanager, string name)
        {
            region = regionmanager.Regions[/*"ContentRegion"*/name];
            return region;
        }

        private void ShowRegion(IRegionManager regionmanager)
        {
            regionmanager.RegisterViewWithRegion("ContentRegion2", typeof(RegionControl));

            //通过name将ContentRegion的区域显示到名为Ctr的ContentControl
            RegionManager.SetRegionName(MainWindow.ctr, "ContentRegion2");
        }

        private void GetRegion()
        {
            var region = GetRegion(regionManager, "ContentRegion");
            //RegionManager.SetRegionContext(MainWindow.ctr, region);
            //regionManager.AddToRegion("ContentRegion", MainWindow.ctr);

            regionManager.Regions["ContentRegion"].RequestNavigate(region.Name);

        }
    }
}

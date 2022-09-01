using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Prism.Regions;
using WpfPrism.UserControls;

namespace WpfPrism.ViewModels
{
  public  class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
       // private readonly IRegionManager regionManager ;

        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        //public MainWindowViewModel(IRegionManager regionmanager)
        //{
        //    this.regionManager = regionmanager;
        //    regionmanager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        //}
        public MainWindowViewModel()
        {

        }
    }
}

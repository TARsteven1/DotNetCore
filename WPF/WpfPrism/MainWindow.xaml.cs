using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPrism.UserControls;
using WpfPrism.ViewModels;

namespace WpfPrism
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private  readonly IRegionManager regionManager;
        //public  IRegion region;
        public static ContentControl ctr;

        public MainWindow(IRegionManager regionmanager)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(regionmanager);
            //#region RegionPart
            ////将RegionControl用户控件注册进ContentRegion中
            //this.regionManager = regionmanager;
            //regionmanager.RegisterViewWithRegion("ContentRegion", typeof(RegionControl));
            //regionmanager.RegisterViewWithRegion("ContentRegion2", typeof(RegionControl));
            ctr = Ctr;
            ////通过name将ContentRegion的区域显示到名为Ctr的ContentControl
            //RegionManager.SetRegionName(Ctr, "ContentRegion2");
            //#endregion

        }


        ////对区域的访问
        //public IRegion GetRegion(IRegionManager regionmanager ,string name)
        //{
        //    region = regionmanager.Regions[/*"ContentRegion"*/name];
        //    return region;
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var region = GetRegion(regionManager, "ContentRegion");
        //}
    }
}

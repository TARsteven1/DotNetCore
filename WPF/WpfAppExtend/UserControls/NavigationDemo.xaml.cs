using GalaSoft.MvvmLight.Messaging;
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
//using WpfAppExtend.ViewModel;

namespace WpfAppExtend.UserControls
{
    /// <summary>
    /// NavigationDemo.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationDemo : UserControl
    {
        public NavigationDemo()
        {
            InitializeComponent();
            //this.DataContext = new MainViewModel();

            //注册通知，将本窗口与自定义token（GetData）和GetData方法链接上
            Messenger.Default.Register<string>(this,"GetData", GetData);
        }
        private void GetData(string v)
        {

            MessageBox.Show("当前窗口"+v);
        }
    }
}

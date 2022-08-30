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

namespace WpfAppExtend.UserControls
{
    /// <summary>
    /// ResourceBase.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceBase : UserControl
    {
        public ResourceBase()
        {
            InitializeComponent();
            //this.Resources.MergedDictionaries.Add(share)
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //根据对应的key找到目标资源吗，更改资源的属性
            this.Resources["SolidColor"] = new SolidColorBrush(Colors.Black);
            //查找资源
            var solidColor = App.Current.FindResource("SolidColor");
            var style = App.Current.FindResource("SolidColorStyle");
        }
    } 
}

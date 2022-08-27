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

namespace WpfAppExtend.UserControl
{
    /// <summary>
    /// Adornor.xaml 的交互逻辑
    /// </summary>
    public partial class Adornor /*: UserControl*/
    {
        public Adornor()
        {
            InitializeComponent();
        }

        private void AddAdorner(object sender, RoutedEventArgs e)
        {
            //layer.Add(new AdornerClass(TargetBtn));
            //var layer = AdornerLayer.GetAdornerLayer(TargetBtn);
            foreach (UIElement item in MyPanel.Children)
            {
                var layer = AdornerLayer.GetAdornerLayer(item);
                layer.Add(new AdornerClass(item));
            }
        }

        private void RemoveAdorner(object sender, RoutedEventArgs e)
        {
            foreach (UIElement item in MyPanel.Children)
            {
                var layer = AdornerLayer.GetAdornerLayer(item);
                var arr = layer.GetAdorners(item);
                if (arr != null)
                {
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        layer.Remove(arr[i]);
                        //layer.Remove(new AdornerClass(arr[i]));
                    }
                }
            }
        }
    }
}

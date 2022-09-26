using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppBasical
{
    /// <summary>
    /// 创建依赖对象，通过其依赖属性进行数据绑定
    /// </summary>
   public class propdpClass :DependencyObject
    {


        public string MyValue
        {
            get { return (string)GetValue(MyValueProperty); }
            set { SetValue(MyValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyValueProperty =
            DependencyProperty.Register("MyValue", typeof(string), typeof(propdpClass), new PropertyMetadata("默认值"));


    }
}

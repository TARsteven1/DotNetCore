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
    /// DependencyPropertyControl.xaml 的交互逻辑
    /// </summary>
    public partial class DependencyPropertyControl : UserControl
    {
        public DependencyPropertyControl()
        {
            InitializeComponent();
            PassWord = "InitValue";
            this.DataContext = this;
        }
        private string password;

        public string PassWord
        {
            get { return password; }
            set { password = value; }
        }
    }

    //创建依赖属性的类，必须继承自DependencyObject
    public class TestDP : DependencyObject
    {
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }
        //创建依赖属性,可以自定义控件，并创建其属性
        public static readonly DependencyProperty dependencyProperty =
            DependencyProperty.Register("MyProperty", typeof(int), typeof(TestDP), new PropertyMetadata(0));



        //附加属性：该对象自身没有的属性，但是父对象附加给他的属性
        //创建附加属性,快捷键：propa+Tab X 2； 定义附加属性无需继承DependencyObject
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(TestDP), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                var pwd = s as PasswordBox;
                pwd.Password = e.NewValue.ToString();
            }
        )));


    }
}

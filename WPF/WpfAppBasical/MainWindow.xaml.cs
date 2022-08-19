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

namespace WpfAppBasical
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new Model();
        public MainWindow()
        {
            InitializeComponent();
            List<Student> students = new List<Student>();
            students.Add(new Student() { UserName = "小王", ClassName = "高二三班", Address = "广州市" });
            students.Add(new Student() { UserName = "小李", ClassName = "高三六班", Address = "清远市" });
            students.Add(new Student() { UserName = "小张", ClassName = "高一一班", Address = "深圳市" });
            students.Add(new Student() { UserName = "小黑", ClassName = "高一三班", Address = "赣州市" });
            gd.ItemsSource = students;

            List<Color> ColorList = new List<Color>();
            ColorList.Add(new Color() { Code = "#FF8C00" });
            ColorList.Add(new Color() { Code = "#FF7F50" });
            ColorList.Add(new Color() { Code = "#FF6EB4" });
            ColorList.Add(new Color() { Code = "#FF4500" });
            ColorList.Add(new Color() { Code = "#FF3030" });
            ColorList.Add(new Color() { Code = "#CD5B45" });

            cob.ItemsSource = ColorList;
            lib.ItemsSource = ColorList;

            List<Test> tests = new List<Test>();
            tests.Add(new Test() { Code = "1" });
            tests.Add(new Test() { Code = "2" });
            tests.Add(new Test() { Code = "3" });
            tests.Add(new Test() { Code = "4" });
            tests.Add(new Test() { Code = "6" });
            ic.ItemsSource = tests;

            BindingTxt.DataContext = new Student { UserName="tar"};
            //设置Window的数据上下文
 
            this.DataContext = model;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt.Text = e.NewValue.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.Code = "org";

        }
    }
    public class Student
    {
        public string UserName { get; set; }
        public string ClassName { get; set; }
        public string Address { get; set; }
    }
    public class Color
    {
        public string Code { get; set; }
    }    
    public class Test
    {
        public string Code { get; set; }
    }
}

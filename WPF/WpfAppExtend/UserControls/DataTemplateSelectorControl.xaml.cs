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
    /// DataTemplateSelectorControl.xaml 的交互逻辑
    /// </summary>
    public partial class DataTemplateSelectorControl : UserControl
    {
        public DataTemplateSelectorControl()
        {
            InitializeComponent();
            List<Student> stuList = new List<Student>();
            stuList.Add( new Student() { Name = "Tar", Score = 40 });
            stuList.Add( new Student() { Name = "Tar1", Score = 50 });
            stuList.Add( new Student() { Name = "Tar2", Score = 60 });
            stuList.Add( new Student() { Name = "Tar3", Score = 70 });

            list.DataContext = new
            {
                Model = stuList
            };
        }
    }
    public class Student
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}

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
using WpfControlLibrary;

namespace HelloWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Btn.Click += new RoutedEventHandler(Button_click2);
        }
        private void Button_click2(object sender, RoutedEventArgs e)
        {
                MessageBox.Show("ok");
        }
            private void Button_click(object sender, RoutedEventArgs e)
        {
            Human h = this.FindResource("human") as Human;
            if (h!=null)
            {
                MessageBox.Show(h.Name);
            }
        }
    }
    public class Human
    {
        public string Name{ get; set; }
        public Human Child { get; set; }
    }
}

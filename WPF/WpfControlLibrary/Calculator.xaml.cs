using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary
{
    /// <summary>
    /// Calculator.xaml 的交互逻辑
    /// </summary>
    public partial class Calculator : UserControl
    {
        public Calculator()
        {
            InitializeComponent();
        }
        private void Button_Click_Calcu(object sender, RoutedEventArgs e)
        {
            this.TextBox3.Text = this.TextBox2.Text + this.TextBox1.Text;
        }
    }
}

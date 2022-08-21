using GalaSoft.MvvmLight.Command;
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
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// AddUser.xaml 的交互逻辑
    /// </summary>
    public partial class AddUser : Window
    {
        //public string TempName;
        public AddUser(Student student)
        {
            InitializeComponent();
           // txt.Text = student.Name;
            this.DataContext = new { Model=student};
            //CommitCommand = new RelayCommand<string>(t=> {  this.DialogResult = true; });
            ////student.Name = txt.Text;
        }

       // public RelayCommand<string> CommitCommand { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }
    }
}

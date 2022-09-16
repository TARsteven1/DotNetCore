using MyToDo.ViewModels;
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
using MyToDo.Common.Event;
using System.Windows.Shapes;
using Prism.Events;
using MyToDo.Extensions;
using MyToDo.Common.Interfaces;

namespace MyToDo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public static ListBox listBox;
        public MainView(IEventAggregator aggregator, IDialogHostService dialogHost)
        {
            InitializeComponent();
            //注册提示消息
            aggregator.RegisterMessage(arg => {
                SnackBar.MessageQueue.Enqueue(arg.Message);
            });
            
            //注册等待消息窗口
            aggregator.Register(arg=> { DialogHost.IsOpen = arg.IsOpen;
                if (DialogHost.IsOpen)
                {
                    DialogHost.DialogContent = new ProgressView();
                }
            });
            //this.DataContext = new MainViewModel();
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
                else this.WindowState = WindowState.Maximized;
            };
            btnClose.Click += async (s, e) => {
                var dialogResult = await dialogHost.Question("提示", "退出到系统?");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
                this.Close(); };

            TopColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            TopColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
                else this.WindowState = WindowState.Normal;
            };
            //点击左侧导航后自动收缩导航
            menuBar.SelectionChanged += (s, e) => { NavDrawer.IsLeftDrawerOpen = false; };
            listBox = menuBar;
        }
    }
}

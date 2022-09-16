﻿using Prism.Events;
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
using MyToDo.Extensions;
using System.Windows.Shapes;

namespace MyToDo.Views.Dialogs
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public LoginView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;
            //注册消息提示
            eventAggregator.RegisterMessage(arg=> {
                LoginSnackbar.MessageQueue.Enqueue(arg.Message);
            },"Login");
        }
    }
}

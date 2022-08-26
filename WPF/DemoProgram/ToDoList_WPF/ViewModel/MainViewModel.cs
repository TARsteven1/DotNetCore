using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_WPF.Common;
using ToDoList_WPF.Models;

namespace ToDoList_WPF.ViewModel
{
  public  class MainViewModel
    {
        private ObservableCollection<MenuItem> menuItems;

        public ObservableCollection<MenuItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }
        public TaskItem taskitem;
        public string Color;
        public MainViewModel()
        {
            Color = "#FF3E8E6C";
            taskitem = new TaskItem();
            MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(new MenuItem() {Icon= "\xe635", Name= "我的一天", Count=1 ,BackColor="#FF3E8E6C"});
            MenuItems.Add(new MenuItem() {Icon= "\xe6b6", Name= "重要的", Count=1, BackColor = "#FFAC395D" });
            MenuItems.Add(new MenuItem() {Icon= "\xe6e1", Name= "已计划日程", Count=1, BackColor = "#FF3E8E6C" });
            MenuItems.Add(new MenuItem() {Icon= "\xe614", Name= "已分配给你", Count=1,BackColor="#FFAC395D" });
            MenuItems.Add(new MenuItem() {Icon= "\xe755", Name= "任务", Count=1, BackColor = "#FF3E8E6C" });
        }
    }
}

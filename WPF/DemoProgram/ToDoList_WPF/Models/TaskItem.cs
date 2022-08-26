using System;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ToDoList_WPF.Models
{
    public class TaskItem :BindableBase
    {
        private string background;
        public string Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Desc { get; set; }
        public string BackColor { get => background; set { background = value; RaisePropertyChanged(); } }

        private ObservableCollection<TaskInfo> tasks;
        public ObservableCollection<TaskInfo> Tasks { get => tasks; set { tasks = value;RaisePropertyChanged(); } }
        public TaskItem()
        {

        }
    }
}

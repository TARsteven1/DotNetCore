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
        private string title;
        private string date;
        private string desc;
        public string Id { get; set; }
        public string Title { get => title; set { title = value; RaisePropertyChanged(); } }
        public string Date { get => date; set { date = value; RaisePropertyChanged(); } }
        public string Desc { get => desc; set { desc = value; RaisePropertyChanged(); } }
        public string BackColor { get => background; set { background = value; RaisePropertyChanged(); } }

        private ObservableCollection<TaskInfo> tasks;
        public ObservableCollection<TaskInfo> Tasks { get => tasks; set { tasks = value;RaisePropertyChanged(); } }
        public TaskItem()
        {

        }
    }
}

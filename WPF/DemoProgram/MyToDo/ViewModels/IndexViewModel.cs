using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyToDo.Common.Models;

namespace MyToDo.ViewModels
{
    public class IndexViewModel:BindableBase
    {
        public IndexViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBar();
        }
        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }
        void CreateTaskBar()
        {
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Target = "IndexView" ,Color="#FF0CA0FF",Count="9"});
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Target = "ToDoView", Color = "#FF1ECA3A", Count = "9" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Target = "MemoView", Color = "#FF02C6DC", Count = "100%" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Target = "SettingsView", Color = "#FFFFA000", Count = "19" });
        }
    }
}

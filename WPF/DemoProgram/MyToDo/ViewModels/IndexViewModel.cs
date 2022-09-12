using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyToDo.Common.Models;
using MyToDo.Shared.Dtos;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private string info;

        public string Info
        {
            get { return "今天是"+System.DateTime.Now.ToString("yyyy-MM-dd")+" "+ System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek); }
            set { info = value; RaisePropertyChanged(); }
        }
        public IndexViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBar();

            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
        }
        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }
        void CreateTaskBar()
        {
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Target = "IndexView", Color = "#FF0CA0FF", Count = "9" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Target = "ToDoView", Color = "#FF1ECA3A", Count = "9" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Target = "MemoView", Color = "#FF02C6DC", Count = "100%" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Target = "SettingsView", Color = "#FFFFA000", Count = "19" });
        }

        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }        
        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
    }
}

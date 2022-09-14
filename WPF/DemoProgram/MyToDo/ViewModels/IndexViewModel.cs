using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyToDo.Common.Models;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Services.Dialogs;
using MyToDo.Common.Interfaces;
using Prism.Ioc;
using MyToDo.Service;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IDialogHostService service;
        private readonly IContainerProvider containerProvider;
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        private string info;

        public string Info
        {
            get { return "今天是" + System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek); }
            set { info = value; RaisePropertyChanged(); }
        }
        public IndexViewModel(IContainerProvider containerProvider,IDialogHostService service) : base(containerProvider)
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBar();

            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.service = service;
            this.toDoService = containerProvider.Resolve<IToDoService>();
            this.memoService = containerProvider.Resolve<IMemoService>();
            this.containerProvider = containerProvider;
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
        public DelegateCommand<string> ExecuteCommand { private set; get; }
        

        private async void Execute(string obj)
        {
            switch (obj)
            {
                case "AddToDo":
                    var dialogToDoResult = await service.ShowDialog("AddToDoView", null);
                    if (dialogToDoResult.Result == Prism.Services.Dialogs.ButtonResult.OK)
                    {
                        var todo = dialogToDoResult.Parameters.GetValue<ToDoDto>("Value");
                        if (todo.Id > 0)
                        {
                            //更新
                        }
                        else
                        {
                            //新增
                            var addResult = await toDoService.AddAsync(todo);
                            if (addResult.Status)
                            {
                                ToDoDtos.Add(addResult.Result);
                            }
                        }
                    }
                    break;
                case "AddMemo":
                    var dialogMemoResult = await service.ShowDialog("AddMemoView", null);
                    if (dialogMemoResult.Result == Prism.Services.Dialogs.ButtonResult.OK)
                    {
                        var memo = dialogMemoResult.Parameters.GetValue<MemoDto>("Value");
                        if (memo.Id > 0)
                        {
                            //更新
                        }
                        else
                        {
                            //新增
                            var addResult = await memoService.AddAsync(memo);
                            if (addResult.Status)
                            {
                                MemoDtos.Add(addResult.Result);
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }
}

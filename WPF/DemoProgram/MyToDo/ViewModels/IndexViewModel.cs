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
using Prism.Regions;
using MyToDo.Extensions;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IDialogHostService service;
        //private readonly IContainerProvider containerProvider;
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        private readonly IRegionManager region;
        private IRegionNavigationJournal Journal;
        private string info;

        public string Info
        {
            get { return "今天是" + System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek); }
            set { info = value; RaisePropertyChanged(); }
        }
        public IndexViewModel(IContainerProvider containerProvider,IDialogHostService service) : base(containerProvider)
        {
            CreateTaskBar();
            //ToDoDtos = new ObservableCollection<ToDoDto>();此处同意使用Summary来接收数据
            //MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.service = service;
            this.toDoService = containerProvider.Resolve<IToDoService>();
            this.memoService = containerProvider.Resolve<IMemoService>();
            //this.containerProvider = containerProvider;
            this.region = containerProvider.Resolve<IRegionManager>();
            EditToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            FinishToDoCommand = new DelegateCommand<ToDoDto>(Finished);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);

        }

        private void Navigate(TaskBar obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Target)) return;
            NavigationParameters pairs = new NavigationParameters();

            if (obj.Title=="已完成")
            {
                pairs.Add("Status", 2);
            }
            region.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.Target, back =>
            {
                Journal = back.Context.NavigationService.Journal;
            }, pairs);
        }

        private async void Finished(ToDoDto obj)
        {
          var updateResult=await toDoService.UpdateAsync(obj);
            if (updateResult.Status)
            {
              var todo=  Summary.ToDoList.FirstOrDefault(t => t.Id.Equals(obj.Id));
                if (todo!=null)
                {
                    Summary.ToDoList.Remove(todo);
                    Summary.FinishedCount += 1;
                    Summary.FinishedRatio = (Summary.FinishedCount / (double)Summary.Sum).ToString("0%");
                    this.RefreshData();
                }
            }
        }

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }
        void CreateTaskBar()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Target = "ToDoView", Color = "#FF0CA0FF" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Target = "ToDoView", Color = "#FF1ECA3A" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Target = "SettingsView", Color = "#FF02C6DC" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Target = "MemoView", Color = "#FFFFA000" });
        }

        //private ObservableCollection<ToDoDto> toDoDtos;

        //public ObservableCollection<ToDoDto> ToDoDtos
        //{
        //    get { return toDoDtos; }
        //    set { toDoDtos = value; RaisePropertyChanged(); }
        //}
        //private ObservableCollection<MemoDto> memoDtos;


        //public ObservableCollection<MemoDto> MemoDtos
        //{
        //    get { return memoDtos; }
        //    set { memoDtos = value; RaisePropertyChanged(); }
        //}
        //首页统计数据接收实体
        private SummaryDto summary;


        public SummaryDto Summary
        {
            get { return summary; }
            set { summary = value; RaisePropertyChanged(); }
        }
        public DelegateCommand<string> ExecuteCommand { private set; get; }
        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }
        public DelegateCommand<ToDoDto> FinishToDoCommand { get; private set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }


        async void AddToDo(ToDoDto tododto)
        {
            DialogParameters pairs = new DialogParameters();
            if (tododto!=null)
            {
                pairs.Add("Value", tododto);
            }
            var dialogToDoResult = await service.ShowDialog("AddToDoView", pairs);
            if (dialogToDoResult.Result == Prism.Services.Dialogs.ButtonResult.OK)
            {
                var todo = dialogToDoResult.Parameters.GetValue<ToDoDto>("Value");
                if (todo.Id > 0)
                {
                    //更新
               var updateResult=await toDoService.UpdateAsync(todo);
                    if (updateResult.Status)
                    {
                       var todoModel = Summary.ToDoList.FirstOrDefault(t => t.Id.Equals(todo.Id));
                        if (todoModel!=null)
                        {
                            todoModel.Title = todo.Title;
                            todoModel.Content = todo.Content;
                        }
                    }
                }
                else
                {
                    //新增
                    var addResult = await toDoService.AddAsync(todo);
                    if (addResult.Status)
                    {
                        Summary.Sum += 1;
                        Summary.ToDoList.Add(addResult.Result);
                        Summary.FinishedRatio = (Summary.FinishedCount / (double)Summary.Sum).ToString("0%");
                        this.RefreshData();
                    }
                }
            }
        }
        async void AddMemo(MemoDto memodto)
        {
            DialogParameters pairs = new DialogParameters();
            if (memodto != null)
            {
                pairs.Add("Value", memodto);
            }
            var dialogMemoResult = await service.ShowDialog("AddMemoView", pairs);
            if (dialogMemoResult.Result == Prism.Services.Dialogs.ButtonResult.OK)
            {
                var memo = dialogMemoResult.Parameters.GetValue<MemoDto>("Value");
                if (memo.Id > 0)
                {
                    //更新
                    var updateResult = await memoService.UpdateAsync(memo);
                    if (updateResult.Status)
                    {
                        var memoModel = Summary.MemoList.FirstOrDefault(t => t.Id.Equals(memo.Id));
                        if (memoModel != null)
                        {
                            memoModel.Title = memo.Title;
                            memoModel.Content = memo.Content;
                        }
                    }
                }
                else
                {
                    //新增
                    var addResult = await memoService.AddAsync(memo);
                    if (addResult.Status)
                    {
                        Summary.MemoList.Add(addResult.Result);
                        Summary.MemoCount += 1;
                        this.RefreshData();
                    }
                }
            }
        }
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "AddToDo":
                    AddToDo(null);
                    break;
                case "AddMemo":
                    AddMemo(null);
                    break;
             
            }
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var summaryResult = await toDoService.SummaryAsync();
            if (summaryResult.Status)
            {
                Summary = summaryResult.Result;
                RefreshData();
            }
            base.OnNavigatedTo(navigationContext);
        }
        void RefreshData()
        {
            taskBars[0].Count = summary.Sum.ToString();
            taskBars[1].Count = summary.FinishedCount.ToString();
            taskBars[2].Count = summary.FinishedRatio;
            taskBars[3].Count = summary.MemoCount.ToString();
        }
    }
}

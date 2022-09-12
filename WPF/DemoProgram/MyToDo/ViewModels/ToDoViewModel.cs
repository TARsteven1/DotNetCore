using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Prism.Commands;
using MyToDo.Shared.Parameters;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using Prism.Ioc;
using Prism.Regions;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        public ToDoViewModel(IToDoService service, IContainerProvider povider):base(povider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            OpenRightDrawerCommand = new DelegateCommand(() => { IsRightDrawerOpen = !IsRightDrawerOpen; });
            this.service = service;
            //CreateToDoList();
        }
        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 获取服务器数据
        /// </summary>
        async void GetDataAsync()
        {
            UpdateLoading(true);
            var todoResult = await service.GetAllAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100
            });
            ToDoDtos.Clear();
            if (todoResult.Status)
            {
                foreach (var item in todoResult.Result.Items)
                {
                    ToDoDtos.Add(item);
                }
            }
            //for (int i = 0; i < 20; i++) /*静态测试数据*/
            //{
            //    ToDoDtos.Add(new ToDoDto() { Title = "测试数据" + i, Content = "嘻嘻嘻嘻嘻嘻寻寻"/*, Color = "#FF0CA0FF" */});
            //}
            UpdateLoading(false);

        }
        public DelegateCommand OpenRightDrawerCommand { private set; get; }
        private bool isRightDrawerOpen;
        private readonly IToDoService service;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }
    }
}

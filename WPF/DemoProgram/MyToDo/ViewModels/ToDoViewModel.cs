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
using MyToDo.Common.Interfaces;
using MyToDo.Extensions;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogHost;
        public ToDoViewModel(IToDoService service, IContainerProvider povider) : base(povider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            //OpenRightDrawerCommand = new DelegateCommand(() => { IsRightDrawerOpen = !IsRightDrawerOpen; });
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.service = service;
            dialogHost = povider.Resolve<IDialogHostService>();
            //CreateToDoList();
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
        }

        private async void Delete(ToDoDto obj)
        {
            try
            {
                var dialogResult = await dialogHost.Question("提示", $"确认删除待办事项:{obj.Title}?");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
                UpdateLoading(true);

                var delResult = await service.DeleteAsync(obj.Id);
                if (delResult.Status)
                {
                    var model = ToDoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                    {
                        ToDoDtos.Remove(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { UpdateLoading(false); }
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增":
                    CommitTxt = "添加到待办事项";
                    CurrentDto = new ToDoDto();
                    IsRightDrawerOpen = true;
                    break;
                case "查询":
                    GetDataAsync(); break;
                case "保存":
                    SaveItem(); break;
                default:
                    break;
            }
        }

        private async void SaveItem()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) || string.IsNullOrWhiteSpace(CurrentDto.Content))
            {
                return;/*提示内容标题不能为空*/
            }
            UpdateLoading(true);
            try
            {
                //id大于0代表是编辑现有项,而不是新增
                if (CurrentDto.Id > 0)
                {
                    var todoResult = await service.UpdateAsync(CurrentDto);
                    if (todoResult.Status)
                    {
                        var todo = ToDoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title;
                            todo.Content = CurrentDto.Content;
                            todo.Status = CurrentDto.Status;
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    var newResult = await service.AddAsync(CurrentDto);
                    if (newResult.Status)
                    {
                        ToDoDtos.Add(newResult.Result);
                        IsRightDrawerOpen = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UpdateLoading(false);
            }

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
            int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;
            var todoResult = await service.GetAllFilterAsync(new ToDoParameters()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
                Status = Status
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
        //public DelegateCommand OpenRightDrawerCommand { private set; get; }
        public DelegateCommand<string> ExecuteCommand { private set; get; }
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

        public DelegateCommand<ToDoDto> SelectedCommand { private set; get; }
        public DelegateCommand<ToDoDto> DeleteCommand { private set; get; }
        private async void Selected(ToDoDto obj)
        {
            try
            {
                CommitTxt = "确认修改";
                UpdateLoading(true);
                var todoResult = await service.GetFirstOrDefaultAsync(obj.Id);
                if (todoResult.Status)
                {
                    CurrentDto = todoResult.Result;
                    IsRightDrawerOpen = true;

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UpdateLoading(false);
            }

        }
        private ToDoDto currentDto;
        //编辑选中的新增对象
        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }
        private string search;
        //搜索条件
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private int selectedIndex;
        //下拉列表选中状态值
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }
        private string commitTxt;

        public string CommitTxt
        {
            get { return commitTxt; }
            set { commitTxt = value; RaisePropertyChanged(); }
        }
    }
}

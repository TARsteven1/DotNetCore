using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Prism.Commands;
using MyToDo.Service;
using MyToDo.Shared.Parameters;
using MyToDo.Shared.Dtos;
using Prism.Ioc;
using Prism.Regions;

namespace MyToDo.ViewModels
{
    public class MemoViewModel :  NavigationViewModel
    {
        public MemoViewModel(IMemoService service, IContainerProvider povider) : base(povider)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            this.service = service;
            //CreateMemoList();
            ExecuteCommand = new DelegateCommand<string>(Execute);

            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
        }
        private async void Delete(MemoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var delResult = await service.DeleteAsync(obj.Id);
                if (delResult.Status)
                {
                    var model = MemoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                    {
                        MemoDtos.Remove(model);
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
                    CommitTxt = "添加到备忘录";
                    CurrentDto = new MemoDto();
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
                        var todo = MemoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title;
                            todo.Content = CurrentDto.Content;
                            //todo.Status = CurrentDto.Status;
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    var newResult = await service.AddAsync(CurrentDto);
                    if (newResult.Status)
                    {
                        MemoDtos.Add(newResult.Result);
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


        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 获取服务器数据
        /// </summary>
        async void GetDataAsync()
        {
            UpdateLoading(true);
            //int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;
            var todoResult = await service.GetAllAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search
                //Status = Status
            });
            MemoDtos.Clear();
            if (todoResult.Status)
            {
                foreach (var item in todoResult.Result.Items)
                {
                    MemoDtos.Add(item);
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
        private readonly IMemoService service;

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

        public DelegateCommand<MemoDto> SelectedCommand { private set; get; }
        public DelegateCommand<MemoDto> DeleteCommand { private set; get; }
        private async void Selected(MemoDto obj)
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
        private MemoDto currentDto;
        //编辑选中的新增对象
        public MemoDto CurrentDto
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

        private string commitTxt;

        public string CommitTxt
        {
            get { return commitTxt; }
            set { commitTxt = value;RaisePropertyChanged(); }
        }

    }
}

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

namespace MyToDo.ViewModels
{
    public class MemoViewModel : BindableBase
    {
        public MemoViewModel(IMemoService service)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            OpenRightDrawerCommand = new DelegateCommand(() => { IsRightDrawerOpen = !IsRightDrawerOpen; });
            this.service = service;
            CreateMemoList();
        }
        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
       async void CreateMemoList()
        {
            var memoResult = await service.GetAllAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100
            });
            MemoDtos.Clear();
            if (memoResult.Status)
            {
                foreach (var item in memoResult.Result.Items)
                {
                    MemoDtos.Add(item);
                }
            }
            //for (int i = 0; i < 20; i++)静态测试数据
            //{
            //    MemoDtos.Add(new MemoDTO() { Title = "测试数据" + i, Content = "嘻嘻嘻嘻嘻嘻寻寻"/*, Color = "#FF0CA0FF" */});
            //}

        }
        public DelegateCommand OpenRightDrawerCommand { private set; get; }
        private bool isRightDrawerOpen;
        private readonly IMemoService service;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

    }
}

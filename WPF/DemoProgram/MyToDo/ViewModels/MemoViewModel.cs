using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Prism.Commands;
using MyToDo.Common.Models;

namespace MyToDo.ViewModels
{
    public class MemoViewModel : BindableBase
    {
        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDTO>();
            CreateMemoList();
            OpenRightDrawerCommand = new DelegateCommand(() => { IsRightDrawerOpen = !IsRightDrawerOpen; });
        }
        private ObservableCollection<MemoDTO> memoDtos;

        public ObservableCollection<MemoDTO> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
        void CreateMemoList()
        {
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new MemoDTO() { Title = "测试数据" + i, Content = "嘻嘻嘻嘻嘻嘻寻寻"/*, Color = "#FF0CA0FF" */});
            }

        }
        public DelegateCommand OpenRightDrawerCommand { private set; get; }
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

    }
}

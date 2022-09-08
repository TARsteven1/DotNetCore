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
   public class ToDoViewModel:BindableBase
    {
        public ToDoViewModel()
        {
            ToDoDtos = new ObservableCollection<ToDoDTO>();
            CreateToDoList();
            OpenRightDrawerCommand = new DelegateCommand(()=> { IsRightDrawerOpen = !IsRightDrawerOpen; });
        }
        private ObservableCollection<ToDoDTO> toDoDtos;

        public ObservableCollection<ToDoDTO> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        void CreateToDoList()
        {
            for (int i = 0; i < 20; i++)
            {

            ToDoDtos.Add(new ToDoDTO() {  Title = "测试数据"+i, Content = "嘻嘻嘻嘻嘻嘻寻寻"/*, Color = "#FF0CA0FF" */});
            }

        }
        public DelegateCommand OpenRightDrawerCommand { private set; get; }
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value;  RaisePropertyChanged(); }
        }


    }
}

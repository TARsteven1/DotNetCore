using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using MyToDo.Common.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using MyToDo.Shared.Dtos;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddToDoViewModel :BindableBase, IDialogHostAware
    {
        public AddToDoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            //取消返回no告诉操作结束
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));

        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(ToDoModel.Title) || string.IsNullOrWhiteSpace(ToDoModel.Content)) return;

            //确定,把编辑的实体返回并返回ok
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters pairs = new DialogParameters();
                pairs.Add("Value",ToDoModel);
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, pairs));
            }

        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                ToDoModel = parameters.GetValue<ToDoDto>("Value");
            }
            else ToDoModel = new ToDoDto();
        }
        private ToDoDto toDoModel;
        //新增或编辑用的实体
        public ToDoDto ToDoModel
        {
            get { return toDoModel; }
            set { toDoModel = value; RaisePropertyChanged(); }
        }

    }
}

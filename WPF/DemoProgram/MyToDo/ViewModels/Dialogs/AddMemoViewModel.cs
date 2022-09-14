using MaterialDesignThemes.Wpf;
using MyToDo.Common.Interfaces;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddMemoViewModel : BindableBase, IDialogHostAware
    {
        public AddMemoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {

            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));

        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(MemoModel.Title) || string.IsNullOrWhiteSpace(MemoModel.Content)) return;

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters pairs = new DialogParameters();
                pairs.Add("Value", MemoModel);
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
                MemoModel = parameters.GetValue<MemoDto>("Value");
            }
            else MemoModel = new MemoDto();
        }
        private MemoDto memoModel;
        //新增或编辑用的实体
        public MemoDto MemoModel
        {
            get { return memoModel; }
            set { memoModel = value; RaisePropertyChanged(); }
        }
    }
}

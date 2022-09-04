using System;
using System.Collections.Generic;
using Prism.Services.Dialogs;
using Prism.Commands;
using System.Text;

namespace WpfPrism.ViewModels
{
    public class DialogViewModel : IDialogAware
    {
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        DialogParameters Pairs = new DialogParameters();
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            if (!Pairs.ContainsKey("value")) Pairs.Add("value", "hi~");
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, Pairs));
        }
        //弹窗打开时触发
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
        }

        //弹窗界面逻辑
        public DialogViewModel()
        {
            OkCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public DelegateCommand OkCommand { set; get; }
        public DelegateCommand CancelCommand { set; get; }
        private void Save()
        {
            OnDialogClosed();
        }
        private void Cancel()
        {
            if (!Pairs.ContainsKey("Cancel"))
            {
                Pairs.Add("Cancel", "Null~");
            }
            RequestClose?.Invoke(new DialogResult(ButtonResult.No, Pairs));

        }
    }
}

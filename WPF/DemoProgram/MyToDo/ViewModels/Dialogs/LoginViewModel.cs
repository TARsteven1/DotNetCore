using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public string Title { set; get; } = "MyToDo";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}

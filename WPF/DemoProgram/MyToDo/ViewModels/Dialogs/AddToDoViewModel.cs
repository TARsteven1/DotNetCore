using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using MyToDo.Common.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using MaterialDesignThemes.Wpf;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddToDoViewModel : IDialogHostAware
    {
        public AddToDoViewModel()
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
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters pairs = new DialogParameters();
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, pairs));
            }

        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}

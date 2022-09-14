using Prism.Services.Dialogs;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Interfaces
{
    public interface IDialogHostAware
    {//所属dialoghost的名称
        string DialogHostName { get; set; }
        //打开过程中执行
        void OnDialogOpend(IDialogParameters parameters);

        DelegateCommand SaveCommand { get; set; }
        DelegateCommand CancelCommand { get; set; }
    }
}

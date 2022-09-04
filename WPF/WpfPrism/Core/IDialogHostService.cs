using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Services.Dialogs;

namespace WpfPrism.Core
{/// <summary>
/// 自定义对话服务接口
/// </summary>
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialogAsync(string name, IDialogParameters Parameters, Action<IDialogResult> callback);

    }
}

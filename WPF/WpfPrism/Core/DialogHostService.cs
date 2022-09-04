using System;
using System.Windows;
using System.Collections.Generic;
using Prism.Services.Dialogs;
using Prism.Ioc;
using Prism.Mvvm;
using System.Text;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace WpfPrism.Core
{/// <summary>
/// 自定义对话服务类
/// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;
        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialogAsync(string name, IDialogParameters Parameters, Action<IDialogResult> callback)
        {
            if (Parameters == null) Parameters = new DialogParameters();
            //从容器中拿到对象实例
            var content = containerExtension.Resolve<object>(name);

            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A Dialog's content must be a FrameworkElement");

            //绑定上下文
            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
            {
                ViewModelLocator.SetAutoWireViewModel(view, true);
            }

            if (!(dialogContent.DataContext is IDialogAware dialogAware))
                throw new NullReferenceException("A Dialog's ViewModel must implement the IDialogAware interfaceIDialogAware");

            if (dialogAware != null) dialogAware.OnDialogOpened(Parameters);
            //往DialogHost的Root节点中设置内容content ,并返回一个点击结果
            //await  DialogHost.Show(content/*,"Root"*/);
            return (IDialogResult)DialogHost.Show(content, "Root");


        }
    }
}

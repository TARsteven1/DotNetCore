using MyToDo.Common.Interfaces;
using Prism.Services.Dialogs;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;

namespace MyToDo.Service
{/// <summary>
/// 自定义对话主机服务:可附加在任何一个DialogHost弹出窗口
/// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension container;

        public DialogHostService(IContainerExtension container) : base(container)
        {
            this.container = container;
        }

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null) parameters = new DialogParameters();
            //从容器中拿到对象实例
            var content = container.Resolve<object>(name);
            //判断是否是FrameworkElement控件类
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A Dialog's content must be a FrameworkElement");

            //绑定上下文
            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
            {
                ViewModelLocator.SetAutoWireViewModel(view, true);
            }
            //判断接口是否是IDialogHostService
            if (!(dialogContent.DataContext is IDialogHostAware dialogAware))
                throw new NullReferenceException("A Dialog's ViewModel must implement the IDialogHostAware interfaceIDialogAware");
            dialogAware.DialogHostName = dialogHostName;
            DialogOpenedEventHandler eventHandler = (sender, eventArgs) => {
                if (dialogAware is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);
                    eventArgs.Session.UpdateContent(content);
                }
            };

            return (IDialogResult)await DialogHost.Show(dialogContent, dialogAware.DialogHostName,eventHandler);
            //if (dialogAware != null) dialogAware.OnDialogOpened(parameters);
            ////往DialogHost的Root节点中设置内容content ,并返回一个点击结果
            ////await  DialogHost.Show(content/*,"Root"*/);
            //return (IDialogResult)DialogHost.Show(content, "Root");

        }
    }
}

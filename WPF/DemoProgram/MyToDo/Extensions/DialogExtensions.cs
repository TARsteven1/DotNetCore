using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Events;
using System.Threading.Tasks;
using MyToDo.Common.Event;
using Prism.Services.Dialogs;
using MyToDo.Common.Interfaces;

namespace MyToDo.Extensions
{
   public static class DialogExtensions
    {/// <summary>
     /// 询问窗口
     /// </summary>
     /// <param name="dialoghost">指定的DialogHost会话主机</param>
     /// <param name="title"></param>
     /// <param name="content"></param>
     /// <param name="dialogHostName">会话主机名称(唯一)</param>
     /// <returns></returns>
        public static async Task<IDialogResult> Question(this IDialogHostService dialoghost, string title,string content, string dialogHostName= "RootDialog")
        {
            DialogParameters pairs = new DialogParameters();
            pairs.Add("Title",title);
            pairs.Add("Content",content);
            pairs.Add("dialogHostName", dialogHostName);

           return await dialoghost.ShowDialog("MessageView", pairs,dialogHostName);
        }
        public static void UpdateLoading(this IEventAggregator aggregator, UpdateModel model)
        {
            //推送等待消息
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
        }        
        public static void Register(this IEventAggregator aggregator,Action<UpdateModel> action)
        {
            //注册等待消息
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
        }        
        public static void RegisterMessage(this IEventAggregator aggregator,Action<MessageModel> action,string filterName="Main")
        {
            //注册提示消息事件(添加注册消息时的过滤器)
            aggregator.GetEvent<MessageEvent>().Subscribe(action,ThreadOption.PublisherThread,true,(m)=> {
                return m.Filter.Equals(filterName);
            });
        }        
        public static void SendMessage(this IEventAggregator aggregator,string message,string filterName = "Main")
        {
            //注册提示消息事件(发送消息默认发送到首页)
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel() { 
            Filter= filterName,
            Message= message
            });
        }


    }
}

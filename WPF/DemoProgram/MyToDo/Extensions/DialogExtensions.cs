using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Events;
using System.Threading.Tasks;
using MyToDo.Common.Event;

namespace MyToDo.Extensions
{
   public static class DialogExtensions
    {
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
    }
}

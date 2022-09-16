using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace MyToDo.Common.Event
{
    public class MessageModel
    {
        public string Filter { set; get; }
        public string Message { set; get; }
    }

    public class MessageEvent : PubSubEvent<MessageModel>
    {

    }
}

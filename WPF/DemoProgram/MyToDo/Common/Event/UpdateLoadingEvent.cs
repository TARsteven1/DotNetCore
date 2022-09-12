using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace MyToDo.Common.Event
{
    public class UpdateLoadingEvent:PubSubEvent<UpdateModel>
    {
    }
    public class UpdateModel
    {
        public bool IsOpen { set; get; }
    }
}

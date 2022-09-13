using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Parameters
{
   public  class ToDoParameters : QueryParameter
    {
        //可为空的int
        public int? Status { set; get; }
    }
}

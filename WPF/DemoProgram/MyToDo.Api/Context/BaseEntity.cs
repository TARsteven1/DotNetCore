using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Context
{
    public class BaseEntity
    {
        public int Id { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime UpdateTime { set; get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Context
{
    public class Memo:BaseEntity
    {
        public string Title { set; get; }
        public string Content { set; get; }
        public int Status { set; get; }
    }
}

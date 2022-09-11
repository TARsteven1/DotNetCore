using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Context
{
    public class User: BaseEntity
    {
        public string Account { set; get; }
        public string UserName { set; get; }
        public string PassWord { set; get; }
    }
}

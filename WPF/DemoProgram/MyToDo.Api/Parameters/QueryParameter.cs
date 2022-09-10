using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Parameters
{
    public class QueryParameter
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public string Search { set; get; }
    }
}

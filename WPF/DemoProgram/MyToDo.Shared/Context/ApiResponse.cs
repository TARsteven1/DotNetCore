using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Shared.Context
{
    public class ApiResponse
    {

        public string Message { set; get; }
        public bool Status { set; get; }
        public object Result { set; get; }
    }
    public class ApiResponse<T>
    {
        public string Message { set; get; }
        public bool Status { set; get; }
        public T Result { set; get; }

    }

}

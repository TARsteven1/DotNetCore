using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public class ApiResponse
    {
        public ApiResponse(string msg, bool status = false)
        {
            this.Message = msg;
            this.Status = status;

        }

        public ApiResponse(bool status, object result)
        {
            this.Status = status;
            this.Result = result;
        }
        public string Message { set; get; }
        public bool Status { set; get; }
        public object Result { set; get; }
    }
    //public class ApiResponse<T>
    //{
    //    public string Message { set; get; }
    //    public bool Status { set; get; }
    //    public T Result { set; get; }

    //}

}

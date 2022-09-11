using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{/// <summary>
/// 通用的请求类
/// </summary>
   public class BaseRequest
    {
        public Method Method { set; get; }
        public string Route { set; get; }
        public string ContentType { set; get; } = "application/json";
        public object Parameter { set; get; }

    }
}

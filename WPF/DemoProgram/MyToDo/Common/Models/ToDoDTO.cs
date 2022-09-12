using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{/// <summary>
/// 待办事项实体类(弃用)
/// </summary>
   public class ToDoDTO: BaseDTO
    {

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}

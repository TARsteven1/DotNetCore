using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    /// <summary>
    /// 备忘录实体类
    /// </summary>
   public  class MemoDTO :BaseDTO
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

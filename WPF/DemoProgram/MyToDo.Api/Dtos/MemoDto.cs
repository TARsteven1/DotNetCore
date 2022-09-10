using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Dtos
{

    /// <summary>
    /// 备忘录数据实体
    /// </summary>
    public class MemoDto : BaseDto
    {
        private string title;
        private string content;
        public string Title { set { title = value; OnPropertyChanged(); } get { return title; } }
        public string Content { set { content = value; OnPropertyChanged(); } get { return content; } }
    }
}

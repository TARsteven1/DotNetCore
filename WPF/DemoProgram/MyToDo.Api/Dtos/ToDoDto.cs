using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Dtos
{/// <summary>
/// 待办事项数据实体
/// </summary>
    public class ToDoDto : BaseDto
    {
        private string title;
        private string content;
        private int status;
        public string Title { set { title = value; OnPropertyChanged(); } get { return title; } }
        public string Content { set { content = value; OnPropertyChanged(); } get { return content; } }
        public int Status { set { status = value; OnPropertyChanged(); } get { return status; } }
    }
}

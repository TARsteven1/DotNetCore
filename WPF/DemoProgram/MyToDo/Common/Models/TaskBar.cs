using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MyToDo.Common.Models
{/// <summary>
/// 首页中部按钮菜单实体类
/// </summary>
   public class TaskBar:BindableBase
    {
        private string icon;

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string count;

        public string Count
        {
            get { return count; }
            set { count = value;RaisePropertyChanged(); }
        }        
        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        private string target;

        public string Target
        {
            get { return target; }
            set { target = value; }
        }
        private int navigateNum;

        public int NavigateNum
        {
            get { return navigateNum; }
            set { navigateNum = value;RaisePropertyChanged(); }
        }

    }
}

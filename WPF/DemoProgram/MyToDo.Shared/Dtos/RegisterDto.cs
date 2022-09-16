using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
   public class RegisterDto:BaseDto
    {
        private string userName;

        private string account;

        private string passWord;
        private string newpassWord;
        public string UserName { set { userName = value; OnPropertyChanged(); } get { return userName; } }
        public string Account { set { account = value; OnPropertyChanged(); } get { return account; } }
        public string PassWord { set { passWord = value; OnPropertyChanged(); } get { return passWord; } }
        public string NewPassWord { set { newpassWord = value; OnPropertyChanged(); } get { return newpassWord; } }

    }
}

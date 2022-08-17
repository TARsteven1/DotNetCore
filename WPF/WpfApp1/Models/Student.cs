using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModel;
using GalaSoft.MvvmLight;

namespace WpfApp1.Models
{
    public class Student : ViewModelBase
    {
        private int id;
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int ID
        {
            get { return id; }
            set { id = value; }
        }

    }

}

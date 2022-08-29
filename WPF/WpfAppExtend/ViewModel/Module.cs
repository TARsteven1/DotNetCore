using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppExtend.ViewModel
{
   public class Module : ViewModelBase
    {
        private string name;

        public string ItemName
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        private string color;

        public string ItemColor
        {
            get { return color; }
            set { color = value; RaisePropertyChanged(); }
        }


    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfAppBasical
{
    public class Model : ViewModelBase
    {
        public Model(){
            Code = "www";
            //Task.Run(async() =>{ 
            //    await Task.Delay(1500);
            //    Code = "com"; });

            command = new RelayCommand(() => { Code = "com"; });
        }
        //public string Code { set; get; }
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value;
                // OnPropertyChanged("www");
                RaisePropertyChanged();
            }
        }
        //引入MvvmLight包，使用命令操作数据
        public RelayCommand command { get;private set; }

        ////实现双向绑定
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(string propertyname)
        //{
        //    if (PropertyChanged!=null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        //    }
        //}
    }
}

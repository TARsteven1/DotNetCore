using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppExtend.UserControls;

namespace WpfAppExtend.Class.DataTemplateSelector
{
 public   class DataTemplateSelectorClass : System.Windows.Controls.DataTemplateSelector
    {
        public DataTemplate HightTemplate { get; set; }
        public DataTemplate LowTemplate { get; set; }

        //根据不同的数据展现不同的模板
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var stu = (Student)item;
            if (stu.Score>60)
            {
                return HightTemplate;

            }
            else
            {
                return LowTemplate;
            }
            //return base.SelectTemplate(item, container);
        }

      }


   
}

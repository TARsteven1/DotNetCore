using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfAppExtend.Class
{/// <summary>
/// 值转换器：将数值转换成对应类型
/// </summary>
    public class IDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value!=null)
            {
                string str= value.ToString();
                if (str=="0")
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
                return "Null";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfAppExtend.Class.ValidationRuleClass
{
    public class NotEmptyValidationRule : ValidationRule
    {/// <summary>
    /// 字符串判断逻辑
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
          return  string.IsNullOrWhiteSpace((value ?? "").ToString())  ? new ValidationResult(false, "Error") : ValidationResult.ValidResult;
        }
    }
}

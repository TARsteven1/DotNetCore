using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppExtend.Class.ValidationRuleClass
{
  public interface IValidationExceptionHandler
    {
        bool IsValid { get; set; }
    }
}

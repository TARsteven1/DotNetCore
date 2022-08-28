using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using WpfAppExtend.Class.ValidationRuleClass;

namespace WpfAppExtend.Class
{
   public class ValidationExceptionBehavior : Behavior<FrameworkElement>
    {/// <summary>
    /// 验证器的主逻辑
    /// </summary>
        private int eCount = 0;
        protected override void OnAttached()
        {
            base.OnAttached();
            //添加异常事件通知

            this.AssociatedObject.AddHandler(Validation.ErrorEvent, new EventHandler<ValidationErrorEventArgs>(OnValidationError));
        }
        private void OnValidationError(object sender, ValidationErrorEventArgs e)
        {
            IValidationExceptionHandler handler=null;
            if (AssociatedObject.DataContext is IValidationExceptionHandler)
            {
                handler = this.AssociatedObject.DataContext as IValidationExceptionHandler;
            }
            if (handler == null) return;

            var element = e.OriginalSource as UIElement;
            if (element == null) return;

            if (e.Action==ValidationErrorEventAction.Added)
            {
                eCount++;
            }
            else if(e.Action==ValidationErrorEventAction.Removed)
            {
                eCount--;
            }           
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}

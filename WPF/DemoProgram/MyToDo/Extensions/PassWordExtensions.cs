using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyToDo.Extensions
{/// <summary>
/// 密码不支持数据绑定，写一个拓展方法用附加属性的方式给密码提供绑定支持
/// </summary>
  public class PassWordExtensions
    {
        public static string GetPassWord(DependencyObject obj)
        {
            return (string)obj.GetValue(PassWordProperty);
        }

        public static void SetPassWord(DependencyObject obj, string value)
        {
            obj.SetValue(PassWordProperty, value);
        }

        // Using a DependencyProperty as the backing store for PassWord.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.RegisterAttached("PassWord", typeof(string), typeof(PassWordExtensions), new PropertyMetadata(string.Empty,OnPassWordPropertyChanged));

        static void OnPassWordPropertyChanged(DependencyObject sender,DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs )
        {
            var passWord = sender as PasswordBox;
            string newPassword = (string)dependencyPropertyChangedEventArgs.NewValue;
            if (passWord!=null && passWord.Password!= newPassword)
            {
                passWord.Password = newPassword;
            }
        
        }


    }
    public class PasswordBehavior : Behavior<PasswordBox> {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            string newPassword = PassWordExtensions.GetPassWord(passwordBox);
            if (passwordBox!=null && passwordBox.Password!= newPassword)
            {
                PassWordExtensions.SetPassWord(passwordBox, newPassword);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;

        }
    }

}

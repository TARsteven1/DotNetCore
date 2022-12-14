using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WpfAppExtend.Class.Behavior
{
    public class EffectBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseEnter += AssociatedObject_MouseMove;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            var element = sender as FrameworkElement;
            element.Effect = (Effect)new DropShadowEffect() { Color = Colors.Transparent, ShadowDepth = 0 };

        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            var element = sender as FrameworkElement;
            element.Effect = (Effect)new DropShadowEffect() { Color = Colors.Red, ShadowDepth = 0 };
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseEnter -= AssociatedObject_MouseMove;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        }
    }
}

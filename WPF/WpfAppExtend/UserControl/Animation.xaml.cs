using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppExtend.UserControl
{
    /// <summary>
    /// Animation.xaml 的交互逻辑
    /// </summary>
    public partial class Animation/* : UserControl*/
    {
        public Animation()
        {
            InitializeComponent();
        }


        private void Start(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(CreateDoubleAnimation(Sample1, false, new RepeatBehavior(1), "Width", 30));
            storyboard.Children.Add(CreateDoubleAnimation(Sample2, false, new RepeatBehavior(5), "Height", 30));
            storyboard.Children.Add(CreateDoubleAnimation(Sample3, true, RepeatBehavior.Forever, "Width", 50));
            storyboard.Children.Add(CreateDoubleAnimation(Sample4, true, new RepeatBehavior(10), "Height", 50)); 
            
            storyboard.Children.Add(CreateDoubleAnimation(Sample5, false, new RepeatBehavior(5), "(UIElement.RenderTransform).(TranslateTransform.X)", 10));
            storyboard.Children.Add(CreateDoubleAnimation(Sample7, false, new RepeatBehavior(5), "(UIElement.RenderTransform).(TranslateTransform.Y)", -30));
            storyboard.Children.Add(CreateDoubleAnimation(Sample6, true, RepeatBehavior.Forever, "(UIElement.RenderTransform).(RotateTransform.Angle)", 350));
            storyboard.Children.Add(CreateDoubleAnimation(Sample8, true, RepeatBehavior.Forever, "Opacity", 1));

            storyboard.Begin();
        }
        private Timeline CreateDoubleAnimation(UIElement uIElement, bool autoReverse, RepeatBehavior repeatBehavior, string propertyPath, double by)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            //doubleAnimation.From = 0;//动画起始值
            //doubleAnimation.To = 100;//动画的结束值
            doubleAnimation.Duration = TimeSpan.FromSeconds(2);//动画的播放时间
            doubleAnimation.RepeatBehavior = repeatBehavior;//动画无限循环播放
            doubleAnimation.AutoReverse = autoReverse;//动画循环过渡补帧（倒放）
            doubleAnimation.By = by;//动画变动范围，与F/T不同时用

            Storyboard.SetTarget(doubleAnimation, uIElement);//绑定动画与控件
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(propertyPath));//设置动画的变动属性

            return doubleAnimation;
        }
        private Timeline CreateTransformAnimation(UIElement uIElement, double by, string propertyPath)
        {
            uIElement.RenderTransform = new TranslateTransform(0, 0);//添加位置特性,支持位移
            uIElement.RenderTransform = new RotateTransform(0, 0, 0);//添加位置特性,支持旋转

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.By = by;
            doubleAnimation.Duration = TimeSpan.FromSeconds(2);//动画的播放时间
            Storyboard.SetTarget(doubleAnimation, uIElement);//绑定动画与控件
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(propertyPath));//设置动画的变动属性


            return doubleAnimation;
        }
    }
}

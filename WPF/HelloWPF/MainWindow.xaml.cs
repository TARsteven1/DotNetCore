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


namespace HelloWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Btn.Click += new RoutedEventHandler(Button_click2);
        }
        private void Button_click2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
        }
        private void Button_click(object sender, RoutedEventArgs e)
        {
            Human h = this.FindResource("human") as Human;
            if (h != null)
            {
                MessageBox.Show(h.Name);
            }
        }

        private void DragRectangle_Dragging(object sender, MouseEventArgs e)
        {
            this.Left += this.DragRectangle.X/2;
            this.Top += this.DragRectangle.Y/2;
        }

        private void DragRectangle_DragFinished(object sender, MouseEventArgs e)
        {
            this.Left += this.DragRectangle.X;
            this.Top += this.DragRectangle.Y;
        }
    }
    public class Human
    {
        public string Name { get; set; }
        public Human Child { get; set; }
    }
    public class Indicator : UserControl
    {
        public Indicator()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            Grid g = new Grid();
            this.Content = g;

            List<Path> paths = new List<Path>();
            for (int i = 0; i < 12; i++)
            {
                //绘制图形
                Path path = new Path();
                path.Data = Geometry.Parse("M 0,0 L -10,0 L -10,-60 L 0,-70 L 10,-60 L 10,0 Z");
                path.Stroke = new SolidColorBrush(Colors.DarkGreen);
                path.Fill = new SolidColorBrush(Colors.LawnGreen);
                path.Opacity = .2f;
                //添加位移和旋转
                TransformGroup tg = new TransformGroup();
                path.RenderTransform = tg;
                TranslateTransform translateTransform = new TranslateTransform();
                translateTransform.Y = -50;
                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.Angle = i * 30;
                tg.Children.Add(translateTransform);
                tg.Children.Add(rotateTransform);
                //动画
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = 1.0;
                doubleAnimation.To = .2;
                doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                doubleAnimation.BeginTime = TimeSpan.FromMilliseconds(i * 1000 / 12);
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

                g.Children.Add(path);
                path.BeginAnimation(Path.OpacityProperty, doubleAnimation);
            }
        }
    }
}

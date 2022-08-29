using LiveCharts;
using LiveCharts.Geared;
using LiveCharts.Wpf;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppExtend.UserControls
{
    /// <summary>
    /// Chart_LiveChatrs.xaml 的交互逻辑
    /// </summary>
    public partial class Chart_LiveChatrs : UserControl
    {
        public Chart_LiveChatrs()
        {
            InitializeComponent();
        }
        public SeriesCollection seriesViewsLeft { set; get; }
        public SeriesCollection seriesViewsRight { set; get; }
        //使用LiveChart图表加载1000条数据
        private void LeftChart(object sender, RoutedEventArgs e)
        {
            ChartValues<double> arr = new ChartValues<double>();
            for (int i = 0; i < 1000; i++)
            {
                arr.Add(i);
            }
            seriesViewsLeft = new SeriesCollection();
            seriesViewsLeft.Add(new LineSeries() { Values = arr });
            c1.Series = seriesViewsLeft;
        }

        //使用GearedLiveChart图表加载1000条数据，性能更好
        private void RightChart(object sender, RoutedEventArgs e)
        {
            GearedValues<double> arr = new GearedValues<double>();
            for (int i = 0; i < 1000; i++)
            {
                arr.Add(i);
            }
            seriesViewsLeft = new SeriesCollection();
            seriesViewsLeft.Add(new GLineSeries() { Values = arr });
            c2.Series = seriesViewsLeft;
        }
    }
}

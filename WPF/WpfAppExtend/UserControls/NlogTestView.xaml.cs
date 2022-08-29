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
    /// NlogTestView.xaml 的交互逻辑
    /// </summary>
    public partial class NlogTestView : UserControl
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public NlogTestView()
        {
            InitializeComponent();

            //自发生成异常
            logger.Info("这是一个异常！");
            logger.Warn("这是一个异常！");
            logger.Debug("123");
            logger.Debug("11111111111111111");
            logger.Debug("2");
            //Console.ReadKey();
        }
    }
}

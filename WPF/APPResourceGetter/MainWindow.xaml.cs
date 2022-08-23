using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
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

namespace APPResourceGetter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string targetFilePath;

        public string TargetFilePath
        {
            get { return targetFilePath; }
            set { targetFilePath = value; }
        }
        private string outPath;

        public string OutPath
        {
            get { return outPath; }
            set { outPath = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnGetResource(object sender, RoutedEventArgs e)
        {
            try
            {

                string path = string.Empty;
                DirectoryInfo folder = new DirectoryInfo(TargetFilePath);
                if (string.IsNullOrEmpty(TargetFilePath))
                {
                    foreach (var file in folder.GetFiles("*.dll"))
                    {
                        var assembly = Assembly.LoadFile(file.FullName);
                        // var resourceName = this.GetType().Assembly.GetName().Name + ".g";
                        var resourceName = assembly.GetName().Name + ".g";
                        var manager = new ResourceManager(resourceName, assembly);
                        var resourceSet = manager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                        var dictionaryEntries = resourceSet.OfType<DictionaryEntry>().ToList();
                        dictionaryEntries.ForEach(arg =>
                        {
                            GetImageResource(arg, (Stream)arg.Value);


                        //Baml2006Reader reader = new Baml2006Reader((Stream)arg.Value);
                        //var win = XamlReader.Load(reader) as Window;
                        //Console.WriteLine(win.Name);
                        });
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        void GetImageResource(DictionaryEntry arg, Stream stream)
        {
            string Imgpath = string.Empty;
            //todo:判断输出路径，如果没有就创建


            if (arg.Key.ToString().Contains("png"))
            {
                Bitmap bitmap = new Bitmap((Stream)arg.Value);
                bitmap.Save(Imgpath);
            }
        }
    }
}

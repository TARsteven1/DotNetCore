using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Prism.Commands;
using MyToDo.Common.Models;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using System.Windows;

namespace MyToDo.ViewModels
{
   public class SkinViewModel:BindableBase
    {
        private bool _isDarkTheme = IsTrueTogle();

        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set
            {
                if (SetProperty(ref _isDarkTheme,value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                } }
        }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public DelegateCommand<object> ChangeHueCommand { get;private set; }
        public SkinViewModel()
        {
            //_isDarkTheme = IsTrueTogle();
            //_isDarkTheme = true;
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
        }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;
            ITheme theme = _paletteHelper.GetTheme();
            theme.PrimaryLight = new ColorPair(hue.Lighten());
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue.Darken());
            _paletteHelper.SetTheme(theme);
        }
        static PaletteHelper paletteHelper = new PaletteHelper();
       static ITheme theme = paletteHelper.GetTheme();
        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }
        /// <summary>
        /// 设置个性化ToggleButton的启示按钮状态
        /// </summary>
        /// <returns></returns>
        private static bool IsTrueTogle()
        {
            //var paletteHelper = new PaletteHelper();
            //ITheme theme = paletteHelper.GetTheme();
            if (theme.GetBaseTheme()==BaseTheme.Dark)
            {
                //MessageBox.Show("xxx");
                return true;
            }
            return false;
        }
    }
}

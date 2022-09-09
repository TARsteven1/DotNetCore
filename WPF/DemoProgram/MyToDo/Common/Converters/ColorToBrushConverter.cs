﻿using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;


namespace MyToDo.Common.Converters
{
    [ValueConversion(typeof(Color), typeof(Brush))]
  public  class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            return Binding.DoNothing;

        }
    }
}

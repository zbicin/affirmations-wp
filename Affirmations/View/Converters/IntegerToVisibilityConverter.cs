using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Affirmations.View
{
    public class IntegerToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = ((int)value) > 0;
            //invert
            if (parameter != null 
                && System.Convert.ToBoolean(parameter) == true)
            {
                visible = !visible;
            }

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            
            //invert
            if (parameter != null
                && System.Convert.ToBoolean(parameter) == true)
            {
                visibility = (visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible);
            }

            return visibility == Visibility.Visible ? 1 : 0;
        }

    }
}

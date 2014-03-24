using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Affirmations.View
{
    public class BooleanToVisibilityConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = (bool)value;

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

            return visibility == Visibility.Visible ? true : false;
        }
    }
}

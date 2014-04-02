using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.Phone.Shell;

namespace Affirmations.View
{
    public class IntegerToBarModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int castedInteger = (int)value;

            //invert
            if (parameter != null
                && System.Convert.ToBoolean(parameter) == true)
            {
                castedInteger = castedInteger > 0 ? 1 : 0;
            }

            return castedInteger == 1 ? ApplicationBarMode.Default : ApplicationBarMode.Minimized;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = (ApplicationBarMode)value;

            //invert
            if (parameter != null
                && System.Convert.ToBoolean(parameter) == true)
            {
                mode = (mode == ApplicationBarMode.Default ? ApplicationBarMode.Minimized : ApplicationBarMode.Default);
            }

            return mode == ApplicationBarMode.Default ? true : false;
        }
    }
}

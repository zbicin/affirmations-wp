using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.View
{
    public class IntegerToBooleanConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = ((int)value) > 0;

            //invert
            if (parameter != null
                && System.Convert.ToBoolean(parameter) == true)
            {
                result = !result;
            }

            return result ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;

            //invert
            if (parameter != null
                && System.Convert.ToBoolean(parameter) == true)
            {
                v = !v;
            }

            return v ? 1 : 0;
        }
    }
}

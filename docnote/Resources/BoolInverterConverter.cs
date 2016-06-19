using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace docnote.Resources
{
    class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        //public object Convert(object value, Type targetType, object parameter,
        //    System.Globalization.CultureInfo culture)
        //{
        //    return value.Equals(parameter);
        //}

        //public object ConvertBack(object value, Type targetType, object parameter,
        //    System.Globalization.CultureInfo culture)
        //{
        //    return value.Equals(true) ? parameter : Binding.DoNothing;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace docnote.Resources
{
    public class ComboBoxConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as ComboBoxItem;
            if (val != null)
            {
                var v = (value as ComboBoxItem).Content.ToString();

                try
                {
                    return System.Convert.ToByte(v);
                }
                catch (Exception)
                {
                    return null;
                }

            }
            return null;
        }
    }

    public class ComboBoxStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as ComboBoxItem;
            if (val != null)
            {
                var v = (value as ComboBoxItem).Content.ToString();
                return v;
            }
            return null;
        }
    }

    public class ComboBoxRomeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            switch (value.ToString())
            {
                case "1": return "I";
                case "2": return "II";
                case "3": return "III";
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as ComboBoxItem;
            if (val != null)
            {
                var v = (value as ComboBoxItem).Content.ToString();
                switch (v)
                {
                    case "I": return (byte)1;
                    case "II": return (byte)2;
                    case "III": return (byte)3;
                }
            }
            return null;
        }
    }
}

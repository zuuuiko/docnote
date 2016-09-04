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
                switch (v)
                {
                    case "І": return (byte)1;
                    case "ІІ": return (byte)2;
                    case "ІІІ": return (byte)3;
                    default:
                        try
                        {
                            return System.Convert.ToByte(v);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                }
            }
            return null;
        }
    }
}

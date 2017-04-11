using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace docnote.Resources
{
    public class BoolToUnboundReasonConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "не знятий";

            bool b = System.Convert.ToBoolean(value);

            if (b)
                return "відкріплення";

            return "смерть";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ComboBoxItem)
            {
                var v = (value as ComboBoxItem).Content.ToString();
                if (v == "відкріплення") return true;
                else if (v == "смерть") return false;
                return null;
            }
            return null;
            
        }
    }
}

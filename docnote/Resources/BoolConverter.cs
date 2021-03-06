﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace docnote.Resources
{
    public class BoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            bool b = System.Convert.ToBoolean(value);

            if (b)
                return "1";

            return "2";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ComboBoxItem)
            {
                if ((value as ComboBoxItem).Content.ToString() == "1") return true;
                return false;
            }
            return null;
            
        }
    }
    public class BoolYesNoConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            bool b = System.Convert.ToBoolean(value);

            if (b)
                return "Так";

            return "Ні";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ComboBoxItem)
            {
                if ((value as ComboBoxItem).Content.ToString() == "Так") return true;
                return false;
            }
            return null;

        }
    }
}

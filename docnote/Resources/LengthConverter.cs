using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace docnote.Resources
{
    public class LengthConverter : IValueConverter
    {
        /// <summary>
        /// Converts the name of a property to the maximum string length of its property.
        /// NOTE:
        /// This converter only works with entities in docnote.Model.
        /// </summary>
        /// <param name="value">not used</param>
        /// <param name="targetType">not used</param>
        /// <param name="parameter">The name of the entity property, e.g. "Patient.FirstName"</param>
        /// <param name="culture">not used</param>
        /// <returns>the string length, or 0 if it could not be determined.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int length = 0;
            string name = parameter as string;

            string[] n = name.Split('.');
            if (n.Length == 2)
            {
                length = GetMaxStringLength(n[0], n[1]);
            }

            return length;
        }

        /// <summary>
        /// Get the max string length attribute of a property (using reflection).
        /// </summary>
        /// <param name="entityType">Type of entity</param>
        /// <param name="propertyName">The name of the entity property</param>
        /// <returns>The max string length, or 0 if not applicable.</returns>
        private int GetMaxStringLengthAttribute(Type entityType, String propertyName)
        {
            int length = 0;
            try
            {
                var propertyInfo = entityType.GetProperty(propertyName);
                var attributes = (StringLengthAttribute)propertyInfo.GetCustomAttributes(typeof(StringLengthAttribute), false).First();
                length = attributes.MaximumLength;
            }
            catch
            {
                // An exception is thrown if the type does not have a maximum length attribute,
                // which is true for unlimited strings and all non-string types.
            }
            return length;
        }

        /// <summary>
        /// Get the max string length attribute of a property (using reflection).
        /// Note:
        /// This currently only works for entities in docnote.Model.
        /// </summary>
        /// <param name="entityName">The name of the entity (as string).</param>
        /// <param name="propertyName">The name of the entity property.</param>
        /// <returns>The max string length, or 0 if not applicable.</returns>
        private int GetMaxStringLength(string entityName, string propertyName)
        {
            // Note:
            // The ", docnote.Model" bit is necessary. It won't work without it.
            string fullname = string.Format("docnote.Model.{0}", entityName);

            Type t = Type.GetType(fullname);
            return GetMaxStringLengthAttribute(t, propertyName);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

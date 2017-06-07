using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace docnote.Model
{
    public abstract class Document
    {
        [NotPrintable]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
        [NotPrintable]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        [NotPrintable]
        public int? PatientId { get; set; }
        [NotPrintable]
        public int? DoctorId { get; set; }
        [NotPrintable]
        public Patient Patient { get; set; }
        [NotPrintable]
        public Doctor Doctor { get; set; }

        [NotPrintable]
        [StringLength(20)]
        public virtual string DocumentName { get; set; }

    }

    //[AttributeUsage(AttributeTargets.Property)]
    //public class RomanAttribute : Attribute
    //{
    //}
    public enum ConvertType
    {
        BoolTo12,
        BoolToYesNo,
        NumberToRome
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrintConverterAttribute : Attribute
    {
        ConvertType ct;
        public PrintConverterAttribute(ConvertType convertType)
        {
            ct = convertType;
        }
        public object Convert(object value)
        {
            if (value == null) return value;
            switch (ct)
            {
                case ConvertType.BoolTo12:
                    value = (bool)value ? 1 : 2;
                    break;
                case ConvertType.BoolToYesNo:
                    value = (bool)value ? "Так" : "Ні";
                    break;
                case ConvertType.NumberToRome:
                    switch (value?.ToString())
                    {
                        case "1": value = "I"; break;
                        case "2": value = "II"; break;
                        case "3": value = "III"; break;
                    }
                    break;
            }
            return value;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotPrintableAttribute : Attribute
    {
    }
}
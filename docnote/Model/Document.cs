using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace docnote.Model
{
    public abstract class Document
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public int? PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        [StringLength(20)]
        public virtual string DocumentName { get; set; }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class RomanAttribute : Attribute
    {
    }
}
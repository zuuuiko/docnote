namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CardEntry
    {
        public int Id { get; set; }

        public int? CardId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        public string EntryText { get; set; }

        public virtual Card Card { get; set; }
    }
}

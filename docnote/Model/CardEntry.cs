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

        public int CardId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string EntryText { get; set; }

        public bool? IsHomeVisit { get; set; } = false;

        public virtual Card Card { get; set; }
    }
}

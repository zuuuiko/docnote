using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    [Table("Form_063_o_Table")]
    class Form_063_o : Document
    {
        public override string DocumentName { get; set; } = @"Форма 063/о";

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string TestText { get; set; }
    }
}

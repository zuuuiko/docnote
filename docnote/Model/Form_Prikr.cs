using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    [Table("Form_Prikr_Table")]
    public class Form_Prikr : Document
    {
        public override string DocumentName { get; set; } = @"Прикріплення";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrikrPatientData> PatientsDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Form_Prikr()
        {
            PatientsDatas = new HashSet<PrikrPatientData>();

        }
    }
}

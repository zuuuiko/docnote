using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    [Table("Form_095_o_Table")]
    class Form_095_o : Document
    {
        public override string DocumentName { get; set; } = @"Форма 095/o";

        [StringLength(50)]
        public string HospitalSubordination { get; set; }   //from hospital
        [StringLength(50)]
        public string HospitalNamePostAddress { get; set; } //from hospital
        [StringLength(10)]
        public string EDRPOU { get; set; }                     //from hospital

        [StringLength(10)]
        public string DocumentNumber { get; set; }          //from view
        //public DateTime? IssueDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string HumanType { get; set; }               //from view
        [StringLength(50)]
        public string SchoolPlace { get; set; }             //from patient
        [StringLength(50)]
        public string PatientFLMName { get; set; }          //from patient
        public DateTime? PatientBirthDate { get; set; }     //from patient
       
        public bool? IsСontactWithInfection { get; set; }   //from view

        [StringLength(300)]
        public string DiagnosisAndAbsenceReason { get; set; }           //from view

        public DateTime? IllStartDate1 { get; set; }         //from view
        public DateTime? IllStartDate2 { get; set; }         //from view
        public DateTime? IllEndDate1 { get; set; }           //from view
        public DateTime? IllEndDate2 { get; set; }           //from view
    }
}

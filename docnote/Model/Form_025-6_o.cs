using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    [Table("Form_025_6_o_Table")]
    class Form_025_6_o : Document
    {
        public override string DocumentName { get; set; } = @"Форма 025-6/o";

        //Наказ МОЗ України
        //29 травня 2013 року №435 
        [StringLength(50)]
        public string DecreeMOZDateNumber { get; set; }     //from ??
        [StringLength(50)]
        public string HospitalSubordination { get; set; }   //from hospital
        [StringLength(50)]
        public string HospitalNamePostAddress { get; set; } //from hospital
        public int EDRPOU { get; set; }                     //from hospital
        [StringLength(50)]
        public string OpeningDoctorName { get; set; }       //from view
        [StringLength(20)]
        public string PatientCardNameCode { get; set; }     //from card
        [StringLength(50)]
        public string PatientFLMName { get; set; }          //from patient
        public bool? PatientSex { get; set; }               //from patient
        public DateTime? PatientBirthDate { get; set; }     //from patient
        [StringLength(30)]
        public string PatientStreet { get; set; }           //from address
        [StringLength(10)]
        public string PatientBuilding { get; set; }         //from address
        [StringLength(10)]
        public string PatientApartment { get; set; }        //from address
        public bool? PatientIsWorking { get; set; }         //from patient
        public byte? Contingent { get; set; }               //from view
        public byte? Purpose { get; set; }                  //from view
        public bool? IsFirstVisit { get; set; }             //from view
        public string VisitDatesHospital { get; set; }      //from card
        public byte? CountVisitsHospital { get; set; }
        public string VisitDatesHome { get; set; }          //from card
        public byte? CountVisitsHome { get; set; }
        [StringLength(30)]
        public string DiagnosisMain { get; set; }           //from view
        [StringLength(20)]
        public string DiagnosisCode { get; set; }           //from view
        [StringLength(30)]
        public string DiagnosisSecondary { get; set; }      //from view
        public byte? DiagnosisSeverity { get; set; }        //from view
        public byte? TraumaPlace { get; set; }              //from view
        [StringLength(30)]
        public string OperationName { get; set; }           //from view
        [Roman]
        public byte? DispGroupIsOnRegister { get; set; }    //from view
        [Roman]
        public byte? DispGroupTakenOnRegister { get; set; } //from view
        public DateTime? DispRemovedRegisterDate { get; set; }     //from view
        public byte? DispRemovedRegisterReason { get; set; }//from view
        public DateTime? DispDateOfNextVisit { get; set; }  //from view
        [Roman]
        public byte? InvGroupFirst { get; set; }            //from view
        [Roman]
        public byte? InvGroupChornobyl { get; set; }        //from view
        [Roman]
        public byte? InvGroupConfirmed { get; set; }        //from view
        //[StringLength(20)]
        //public string IllSheetCode { get; set; }            //from view
        public DateTime? IllSheetOpenDate { get; set; }     //from view
        public DateTime? IllSheetCloseDate { get; set; }    //from view
        public DateTime? DovidkaOpenDate { get; set; }      //from view
        public DateTime? DovidkaCloseDate { get; set; }     //from view
        public byte? TreatmentResult { get; set; }          //from view
        public bool? ServiceCase { get; set; }              //from view
        [StringLength(50)]
        public string ClosingDoctorName { get; set; }       //from view
        public DateTime? SignDate { get; set; }             //from view
    }
}

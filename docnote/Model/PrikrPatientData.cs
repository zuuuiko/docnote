using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    public class PrikrPatientData
    {
        public int Id { get; set; }
        public int? RowId { get; set; }
        [StringLength(50)]
        public string FLMName { get; set; }          //from patient
        public DateTime? BirthDate { get; set; }     //from patient
        [StringLength(100)]
        public string IdentificationDocumentDetails { get; set; }     //from patient

        [StringLength(30)]
        public string City { get; set; }           //from address
        [StringLength(30)]
        public string Street { get; set; }           //from address
        [StringLength(10)]
        public string Building { get; set; }         //from address
        [StringLength(10)]
        public string Apartment { get; set; }        //from address

        public DateTime? SignDate { get; set; } = DateTime.Now; 
        public bool? UnboundReasonCode { get; set; } //from patient
        public DateTime? UnboundDate { get; set; } //from patient


        public int? FormPrikrId { get; set; }
        public virtual Form_Prikr FormPrikr { get; set; }
        //public override string ToString()
        //{
        //    return $"{RowId}|{FLMName}|{BirthDate}|{IdentificationDocumentDetails}|{City}|{Street}|{Building}|{Apartment}|{SignDate}|{UnboundReasonCode}|{UnboundDate}";
        //}
    }
}

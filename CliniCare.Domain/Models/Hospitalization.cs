using CliniCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Models
{
    public class Hospitalization
    {

        public int Id { get; set; }
        public HospitalizationStatus Status { get; set; } 
        public DateTime AdmissionDate { get; set; } 
        public DateTime? DischargeDate { get; set; }  
        public string Notes { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}

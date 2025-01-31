using CliniCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Models
{
    public class Scheduling
    {
        public int Id { get; set; }
        public DateTime SchedulingDate { get; set; }
        public TimeSpan SchedulingTime { get; set; }
        public SchedulingsStatus SchedulingsStatus { get; set; }
        public string Observation { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int ProcedureId { get; set; }
        public Procedure Procedure { get; set; }
        public int VeterinarianId { get; set; }
        public Veterinarian Veterinarian { get; set; }
    }
}

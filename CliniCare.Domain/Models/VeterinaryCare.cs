using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Models
{
    public class VeterinaryCare
    {
        public int Id { get; set; }
        public DateTime CareDate { get; set; }
        public string ProblemDescription { get; set; }
        public string Treatment { get; set; }
        public int SchedulingId { get; set; }
        public Scheduling Scheduling { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int VeterinarianId { get; set; }
        public Veterinarian Veterinarian { get; set; }


    }
}

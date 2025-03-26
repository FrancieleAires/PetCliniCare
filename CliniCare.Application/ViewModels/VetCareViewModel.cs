using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.ViewModels
{
    public class VetCareViewModel
    {
        public int Id { get; set; }
        public DateTime CareDate { get; set; }
        public string ProblemDescription { get; set; }
        public string Treatment { get; set; }
        public int SchedulingId { get; set; }
        public string AnimalName { get; set; }
        public string VeterinarianName { get; set; }
    }
}

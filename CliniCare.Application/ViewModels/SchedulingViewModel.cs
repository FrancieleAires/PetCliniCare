using CliniCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.ViewModels
{
    public class SchedulingViewModel
    {
        public int Id { get; set; }
        public DateTime SchedulingDate { get; set; }
        public TimeSpan SchedulingTime { get; set; }
        public SchedulingsStatus SchedulingsStatus { get; set; }
        public string Observation { get; set; }
        public string AnimalName { get; set; }
        public string ProcedureName { get; set; }
        public string VeterinarianName { get; set; }
    }
}

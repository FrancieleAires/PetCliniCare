using CliniCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.InputModels.Scheduling
{
    public class CreateSchedulingInputModel
    {
        public DateTime SchedulingDate { get; set; }
        public TimeSpan SchedulingTime { get; set; }
        public SchedulingsStatus SchedulingsStatus { get; set; }
        public string Observation { get; set; }
        public int ClientId { get; set; }
        public int AnimalId { get; set; }
        public int ServiceId { get; set; }
        public int VeterinarianId { get; set; }


    }
}

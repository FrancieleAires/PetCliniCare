using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.InputModels.VeterinaryCare
{
    public class CreateVetCareInputModel
    {
        public DateTime CareDate { get; set; }
        public string ProblemDescription { get; set; }
        public string Treatment { get; set; }
        public int VeterinarianId { get; set; }
        public int AnimalId { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.InputModels.Hospitalization
{
    public class CreateHospitalizationInputModel
    {
        public DateTime DateOfAdmission { get; set; }
        public DateTime? DateOfDischarge { get; set; }
        public string Observations { get; set; }
        public int AnimalId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Models
{
    public class Procedure
    {
        //model para serviços prestados pela clínica, ex: banho e tosa, consulta veterinária, vacina
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public List<Hospitalization> Hospitalizations { get; set; } = new List<Hospitalization>();
    }
}

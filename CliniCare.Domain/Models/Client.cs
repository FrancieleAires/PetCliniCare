using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public int ApplicationUserId  { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<Animal> Animals { get; set; }
    }
}

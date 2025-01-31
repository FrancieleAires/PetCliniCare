using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Models
{
    public class Veterinarian
    {
        public int Id { get; set; }
        public string CRMV { get; set; }
        public int Specialty { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

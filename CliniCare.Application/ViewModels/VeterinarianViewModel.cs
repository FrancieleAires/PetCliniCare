using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.ViewModels
{
    public class VeterinarianViewModel
    {
        public int Id { get; set; }
        public string CRMV { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; } =  null!;
    }
}

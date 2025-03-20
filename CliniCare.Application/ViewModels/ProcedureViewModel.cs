using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.ViewModels
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}

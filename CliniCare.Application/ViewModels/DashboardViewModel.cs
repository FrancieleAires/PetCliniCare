using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.ViewModels
{
    public class DashboardViewModel
    {
        public DashInfos Info { get; set; }
        public List<SchedulingViewModel> NextSchedulings { get; set; }
        public List<AnimalViewModel> Pets { get; set; }
    }


    public class DashInfos 
    {
        public int AmountNextSchedulings{ get; set; }
        public int AmountFinishedSchedulings { get; set; }
        public int AmountPetsCreated { get; set; }
    }


}

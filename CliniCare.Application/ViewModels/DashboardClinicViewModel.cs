using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.ViewModels
{
    public class DashboardClinicViewModel
    {
        public DashClinicInfo infos { get; set; }
        public List<SchedulingViewModel> CurrentSchedulings { get; set; }

        public class DashClinicInfo
        {
            public int AmountClientsCreated { get; set; }
            public int AmountPendingScheduling { get; set; }
            public int AmountFinishedScheduling { get; set; }
            public int AmountCurrentsShedulings { get; set; }
        }
    }
}

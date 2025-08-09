using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.DashboardClinic
{
    public class DashboardClinicQuery : IRequest<Result<DashboardClinicViewModel>>
    {
        public DashboardClinicQuery()
        {

        }
    }
}

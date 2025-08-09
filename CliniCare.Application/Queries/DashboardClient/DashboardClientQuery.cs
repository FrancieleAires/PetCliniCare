using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.DashboardClient
{
    public class DashboardClientQuery : IRequest<Result<DashboardViewModel>>
    {
        public DashboardClientQuery()
        {
            
        }

    }
}

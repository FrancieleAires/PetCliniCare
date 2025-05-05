using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Schedulings.GetIdScheduling
{
    public class GetSchedulingByIdQuery : IRequest<Result<SchedulingViewModel>>
    {
        public int SchedulingId { get; set; }

        public GetSchedulingByIdQuery(int schedulingId)
        {
            SchedulingId = schedulingId;
        }
    }
}

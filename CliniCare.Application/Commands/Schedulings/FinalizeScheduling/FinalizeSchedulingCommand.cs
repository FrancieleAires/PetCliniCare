using CliniCare.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings.FinalizeScheduling
{
    public class FinalizeSchedulingCommand : IRequest<Result<Unit>>
    {
        public int SchedulingId { get; set; }

        public FinalizeSchedulingCommand(int schedulingId)
        {
            SchedulingId = schedulingId;
        }
    }
}

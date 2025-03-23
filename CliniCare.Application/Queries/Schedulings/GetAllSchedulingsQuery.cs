using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Schedulings
{
    public class GetAllSchedulingsQuery : IRequest<Result<List<SchedulingViewModel>>>
    {
        public GetAllSchedulingsQuery()
        {
        }
    }
}

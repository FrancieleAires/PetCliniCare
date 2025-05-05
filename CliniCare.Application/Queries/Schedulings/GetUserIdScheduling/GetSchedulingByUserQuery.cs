using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Schedulings.GetUserIdScheduling
{
    public class GetSchedulingsByUserQuery : IRequest<Result<List<SchedulingViewModel>>>
    {
        public int UserId { get; set; }

        public GetSchedulingsByUserQuery(int userId)
        {
            UserId = userId;
        }
    }

}

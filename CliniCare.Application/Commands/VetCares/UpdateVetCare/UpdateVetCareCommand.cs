using CliniCare.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.VetCares.UpdateVetCare
{
    public class UpdateVetCareCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public DateTime CareDate { get; set; }
        public string ProblemDescription { get; set; }
        public string Treatment { get; set; }

        public UpdateVetCareCommand(DateTime careDate, string problemDescription, string treatment)
        {
            CareDate = careDate;
            ProblemDescription = problemDescription;
            Treatment = treatment;
        }
    }
}

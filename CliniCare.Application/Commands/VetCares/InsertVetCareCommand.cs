using CliniCare.Application.Helpers;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.VetCare
{
    public class InsertVetCareCommand : IRequest<Result<Unit>>
    {

        public DateTime CareDate { get; set; }
        public string ProblemDescription { get; set; }
        public string Treatment { get; set; }
        public int SchedulingId { get; set; }
        public int AnimalId { get; set; }
        public int VeterinarianId { get; set; }

        public VeterinaryCare ToEntity(int veterinarianId)
        {
            return new VeterinaryCare
            {
                CareDate = CareDate,
                ProblemDescription = ProblemDescription,
                Treatment = Treatment,
                SchedulingId = SchedulingId,
                AnimalId = AnimalId,
                VeterinarianId  = VeterinarianId

            };
        }
    }
}

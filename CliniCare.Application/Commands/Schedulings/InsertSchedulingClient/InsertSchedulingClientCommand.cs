using CliniCare.Application.Helpers;
using CliniCare.Domain.Enums;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings.InsertSchedulingClient
{
    public class InsertSchedulingClientCommand : IRequest<Result<Unit>>
    {
        public DateTime SchedulingDate { get; set; }
        public TimeSpan SchedulingTime { get; set; }
        public string Observation { get; set; }
        public int AnimalId { get; set; }
        public int ProcedureId { get; set; }
        public int VeterinarianId { get; set; }


        public Scheduling ToEntity(int clientId)
        {
            return new Scheduling
            {
                SchedulingDate = SchedulingDate,
                SchedulingTime = SchedulingTime,
                SchedulingsStatus = SchedulingsStatus.Pendente,
                Observation = Observation,
                ClientId = clientId,
                ProcedureId = ProcedureId,
                AnimalId = AnimalId,
                VeterinarianId = VeterinarianId,
            };
        }
    }
}

using CliniCare.Application.Helpers;
using CliniCare.Domain.Enums;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings.InsertSchedulingClinic
{
    public class InsertSchedulingClinicCommand : IRequest<Result<Unit>>
    {
        public DateTime SchedulingDate { get; set; }
        public TimeSpan SchedulingTime { get; set; }
        public string Observation { get; set; }
        public int AnimalId { get; set; }
        public int ProcedureId { get; set; }
        public int VeterinarianId { get; set; }
        public int ClientId { get; set; }

        public Scheduling ToEntity()
        {
            return new Scheduling
            {
                SchedulingDate = SchedulingDate,
                SchedulingTime = SchedulingTime,
                SchedulingsStatus = SchedulingsStatus.Pendente,
                Observation = Observation,
                ClientId = ClientId,
                ProcedureId = ProcedureId,
                AnimalId = AnimalId,
                VeterinarianId = VeterinarianId,
            };
        }
    }
}

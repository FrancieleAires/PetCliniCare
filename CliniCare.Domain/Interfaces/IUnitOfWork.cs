using CliniCare.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        IVeterinarianRepository Veterinarians { get; }
        IAnimalRepository Animals { get; }

        IVeterinaryProcedureRepository VeterinaryProcedures { get; }

        ISchedulingRepository Schedulings { get; }

        IVeterinaryCareRepository VeterinaryCares { get; }


        Task<bool> CommitAsync();
    }
}

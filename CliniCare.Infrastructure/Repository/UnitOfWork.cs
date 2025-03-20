using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Repositories;
using CliniCare.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApiDbContext _context;

    public IClientRepository Clients { get; }
    public IVeterinarianRepository Veterinarians { get; }
    public IAnimalRepository Animals { get; }
    public IVeterinaryProcedureRepository VeterinaryProcedures { get; }

    public UnitOfWork(ApiDbContext context,
                      IClientRepository clientRepository,
                      IVeterinarianRepository veterinarianRepository,
                      IAnimalRepository animalRepository,
                      IVeterinaryProcedureRepository veterinaryProcedures)
    {
        _context = context;
        Clients = clientRepository;
        Veterinarians = veterinarianRepository;
        Animals = animalRepository;
        VeterinaryProcedures = veterinaryProcedures;
    }

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

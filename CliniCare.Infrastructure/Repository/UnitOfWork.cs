using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Repositories;
using CliniCare.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApiDbContext _context;

    public IClientRepository Clients { get; }
    public IVeterinarianRepository Veterinarians { get; }

    public UnitOfWork(ApiDbContext context,
                      IClientRepository clientRepository,
                      IVeterinarianRepository veterinarianRepository)
    {
        _context = context;
        Clients = clientRepository;
        Veterinarians = veterinarianRepository;
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

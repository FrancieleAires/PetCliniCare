﻿using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Repositories;
using CliniCare.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApiDbContext _context;

    public IClientRepository Clients { get; }
    public IVeterinarianRepository Veterinarians { get; }
    public IAnimalRepository Animals { get; }
    public IVeterinaryProcedureRepository VeterinaryProcedures { get; }
    public ISchedulingRepository Schedulings { get; }
    public IVeterinaryCareRepository VeterinaryCares { get; }

    public UnitOfWork(ApiDbContext context,
                      IClientRepository clientRepository,
                      IVeterinarianRepository veterinarianRepository,
                      IAnimalRepository animalRepository,
                      IVeterinaryProcedureRepository veterinaryProcedures,
                      ISchedulingRepository schedulings,
                      IVeterinaryCareRepository veterinaryCares)
    {
        _context = context;
        Clients = clientRepository;
        Veterinarians = veterinarianRepository;
        Animals = animalRepository;
        VeterinaryProcedures = veterinaryProcedures;
        Schedulings = schedulings;
        VeterinaryCares = veterinaryCares;
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

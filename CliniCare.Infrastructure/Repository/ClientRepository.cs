using CliniCare.Domain.Repositories;
using CliniCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliniCare.Infrastructure.Persistence;

namespace CliniCare.Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApiDbContext _dbContext;

        public ClientRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddClientAsync(Client client)
        {
             await _dbContext.Clients.AddAsync(client);
        }

        public async Task DeleteClientAsync(Client client)
        {
             _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAllClientAsync()
        {
            return await _dbContext.Clients
                .Include(c => c.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _dbContext.Clients
            .Include(c => c.ApplicationUser)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void UpdateClient(Client client)
        {
              _dbContext.Clients.Update(client);
              
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Id == id);
        }

        public async Task<Client> GetCurrentClientAsync(int userId)
        {
            return await _dbContext.Clients
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
        }
    }
}

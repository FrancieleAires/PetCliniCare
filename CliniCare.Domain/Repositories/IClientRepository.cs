using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Repositories
{
    public interface IClientRepository
    {
        Task AddClientAsync(Client client);
        void UpdateClient(Client client);
        Task<IEnumerable<Client>> GetAllClientAsync();
        Task<Client> GetCurrentClientAsync(int userId);
        Task<Client> GetClientByUserIdAsync(int userId);
        Task<Client> GetClientByIdAsync (int id);
        Task DeleteClientAsync(Client client);
        Task<bool> ExistsAsync(int id);


    }
}

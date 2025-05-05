using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Repositories
{
    public interface IHospitalizationRepository
    {
        Task<Hospitalization?> GetByIdAsync(int id);
        Task<IEnumerable<Hospitalization>> GetByAnimalIdAsync(int animalId);
        Task AddAsync(Hospitalization hospitalization);
        Task UpdateAsync(Hospitalization hospitalization);
        Task DeleteAsync(int id);
    }
}

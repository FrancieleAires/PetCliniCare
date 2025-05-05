using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Services.Interfaces
{
    public interface IHospitalizationService 
    {
        Task<Hospitalization> AddHospitalizationAsync(Hospitalization hospitalization);
        Task<Hospitalization> UpdateHospitalizationAsync(Hospitalization hospitalization);
        Task<Hospitalization> GetHospitalizationByIdAsync(int id);
    }
}

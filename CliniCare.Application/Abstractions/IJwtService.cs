using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Abstractions
{
    public interface IJwtService
    {
        Task<string> GerarJwt(ApplicationUser user, IList<string> roles);
    }
}

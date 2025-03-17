using CliniCare.Application.Services.Interfaces;
using System.Security.Claims;

namespace CliniCare.API
{
    public class ApplicationUsers : IApplicationUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationUsers(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public int Id => int.Parse(_httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value!);


    }
}

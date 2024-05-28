using Domain.Models;
using Domain.View;

namespace Services.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> LoginAsync(LoginRequestView model);
        Task<ApiResponse> Register(RegisterRequestView model);
    }
}

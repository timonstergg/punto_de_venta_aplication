using Venta.Shared.DTOs.DTOs;

namespace Venta.BlazorUI.Services.Interface
{
    public interface IAuthService
    {
        Task<bool> Login(LoginDTO model);
        Task Logout();
    }
}

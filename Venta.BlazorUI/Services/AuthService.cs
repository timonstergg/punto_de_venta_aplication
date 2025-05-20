using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using Venta.BlazorUI.Services.Interface;
using Venta.Shared.DTOs;
using Venta.Shared.DTOs.DTOs;

using static System.Net.WebRequestMethods;

namespace Venta.BlazorUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;

        }

        public async Task<bool> Login(LoginDTO model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            if (result is null)
                return false;

            await _js.InvokeVoidAsync("localStorage.setItem", "token", result.Token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

            return true;
        }

        public async Task Logout()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "token");
            _http.DefaultRequestHeaders.Authorization = null;
        }
    }
}

using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using Satisfy.Shared;
using Microsoft.Extensions.Configuration;

namespace Satisfy.Web
{
    public class LoginService
    {
        private HttpClient _httlClient;
        private readonly IConfiguration Configuration;
        public LoginService(HttpClient httpClient, IConfiguration configuration)
        {
            _httlClient = httpClient;
            Configuration = configuration;
        }
        public async Task<LoginResponse> Login(string username, string password)
        {
            var login = Configuration["url"];
            LoginResponse response = await _httlClient.PostJsonAsync<LoginResponse>(login+ "api/User/Login", new LoginRequest(username, password));
            return response;
        }

    }
}

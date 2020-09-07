using Microsoft.AspNetCore.Components;
using Satisfy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Satisfy.Web
{
    public class RegistrationService
    {
        private HttpClient _httlClient;
        private readonly IConfiguration Configuration;
        public RegistrationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httlClient = httpClient;
            Configuration = configuration;
        }

        public async Task<RegistrationResponse> Registration(string username, string name, string email, string password, string repeatPassword)
        {
            var signup = Configuration["url"];
            RegistrationResponse response = await _httlClient.PostJsonAsync<RegistrationResponse>(signup+ "api/User/Register", new RegistrationRequest(username,name,email,password,repeatPassword));
            return response;
        }
    }
}

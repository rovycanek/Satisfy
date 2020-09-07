using Microsoft.AspNetCore.Components;
using Satisfy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Satisfy.Web.Data
{
    public class SendMailService
    {
        private HttpClient _httlClient;
        private readonly IConfiguration Configuration;

        public SendMailService(HttpClient httpClient, IConfiguration configuration)
        {
            _httlClient = httpClient;
            Configuration = configuration;
        }
        public async Task<SendMailResponse> SendMail(string to, string subject, string body)
        {
            var SendMail = Configuration["url"];
            SendMailResponse response = await _httlClient.PostJsonAsync<SendMailResponse>(SendMail+ "Email/SendMail", new SendMailRequest(to, subject,body));
            return response;
        }
    }
}

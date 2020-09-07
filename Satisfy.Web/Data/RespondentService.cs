using Microsoft.AspNetCore.Components;
using Satisfy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Satisfy.Shared.Form;
using Satisfy.Shared.Respondent;

namespace Satisfy.Web.Data
{
    public class RespondentService
    {
        private HttpClient _httlClient;
        private readonly IConfiguration Configuration;

        public RespondentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httlClient = httpClient;
            Configuration = configuration;
        }
        public async Task<RespondentAddResponse> AddRespondent(int contactID, int dotaznikID)
        {
            var RespondentAdd = Configuration["url"];
            RespondentAddResponse response = await _httlClient.PostJsonAsync<RespondentAddResponse>(RespondentAdd+ "api/Respondent/RespondentAdd", new RespondentAddRequest(dotaznikID,contactID));
            return response;
        }

        public async Task<QuestionnaireIDResponse> GetDotaznikID(int id)
        {
            var DotaznikID = Configuration["url"];
            QuestionnaireIDResponse response = await _httlClient.PostJsonAsync<QuestionnaireIDResponse>(DotaznikID+ "api/Respondent/QuestionnaireId", new QuestionnaireIDRequest(id));
            return response;
        }

        public async Task<RespondentResponseResponse> SaveResponse(int dotaznikID, int otazkaID, string text, int respondetID, int odpovedID)
        {
            var ResponseAdd = Configuration["url"];
            RespondentResponseResponse response = await _httlClient.PostJsonAsync<RespondentResponseResponse>(ResponseAdd+ "api/Respondent/ResponseAdd", new RespondentResponseRequest(dotaznikID, otazkaID, text, respondetID, odpovedID));
            return response;
        }

        public async Task<RespondentRespondedResponse> Responded(int id)
        {
            var DotaznikID = Configuration["url"];
            RespondentRespondedResponse response = await _httlClient.PostJsonAsync<RespondentRespondedResponse>(DotaznikID + "api/Respondent/Responded", new RespondentRespondedRequest(id));
            return response;
        }
    }
}

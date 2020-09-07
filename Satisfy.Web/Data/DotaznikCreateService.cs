using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Satisfy.Shared.Form;

namespace Satisfy.Web.Data
{
    public class DotaznikCreateService
    {
        private HttpClient _httlClient;
        private readonly IConfiguration _configuration;
        public DotaznikCreateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httlClient = httpClient;
            _configuration = configuration;
        }


        public async Task<QuestionnaireSaveResponse> DotaznikCreate(QuestionnaireSaveRequest questionnaire)
        {
            var apiForm = _configuration["url"];
            var response = await _httlClient.PostJsonAsync<QuestionnaireSaveResponse>(apiForm + "api/Questionnaire/Save", questionnaire);
            return response;
        }


        public async Task<QuestionnaireUpdateResponse> DotaznikUpdate(QuestionnaireUpdateRequest questionnaire)
        {
            var apiForm = _configuration["url"];
            var response = await _httlClient.PostJsonAsync<QuestionnaireUpdateResponse>(apiForm + "api/Questionnaire/Update", questionnaire);

            return response;
        }


        public async Task<QuestionnaireGetResponse> DotaznikLoad(int questionnaireId)
        {
            var apiForm = _configuration["url"];
            var response = await _httlClient.PostJsonAsync<QuestionnaireGetResponse>(
                apiForm + "api/Questionnaire/ById",
                new QuestionnaireGetRequest
                {
                    Questionnaire = new QuestionnaireGetRequest.QuestionnaireGet
                    {
                        QuestionnaireId = questionnaireId
                    }
                });
            return response;
        }


    }
}

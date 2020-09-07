using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
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
    public class DotaznikListService
    {
        private HttpClient _httlClient;
        private readonly IConfiguration Configuration;

        public DotaznikListService(HttpClient httpClient, IConfiguration configuration)
        {
            _httlClient = httpClient;
            Configuration = configuration;
        }

        public async Task<QuestionnaireListResponse> LoadDotazniky(int userID)
        {
            var apiForm = Configuration["url"];
            QuestionnaireListResponse response = await _httlClient.PostJsonAsync<QuestionnaireListResponse>(
                apiForm + "api/Questionnaire/List",
                new QuestionnaireListRequest()
                {
                    Questionnaire = new QuestionnaireListRequest.QuestionnaireList()
                    {
                        UserId = userID
                    }
                });
            return response;
        }


        public async Task<QuestionnairePublishResponse> PublishDotazniky(int questionnaireId)
        {
            var apiForm = Configuration["url"];
            QuestionnairePublishResponse response = await _httlClient.PostJsonAsync<QuestionnairePublishResponse>(
                apiForm + "api/Questionnaire/Publish",
                new QuestionnairePublishRequest()
                {
                    Questionnaire = new QuestionnairePublishRequest.QuestionnairePublish()
                    {
                        QuestionnaireId = questionnaireId
                    }
                });
            return response;
        }


        public async Task<QuestionnaireDeleteResponse> DeleteDotazniky(int questionnaireId)
        {
            var apiForm = Configuration["url"];
            QuestionnaireDeleteResponse response = await _httlClient.PostJsonAsync<QuestionnaireDeleteResponse>(
                apiForm + "api/Questionnaire/Delete",
                new QuestionnaireDeleteRequest()
                {
                    Questionnaire = new QuestionnaireDeleteRequest.QuestionnaireGet
                    {
                        QuestionnaireId = questionnaireId
                    }
                });
            return response;
        }

        public async Task<QuestionnaireCopyResponse> CopyDotazniky(int questionnaireId)
        {
            var apiForm = Configuration["url"];
            QuestionnaireCopyResponse response = await _httlClient.PostJsonAsync<QuestionnaireCopyResponse>(
                apiForm + "api/Questionnaire/Copy",
                new QuestionnaireCopyRequest
                {
                    Questionnaire = new QuestionnaireCopyRequest.QuestionnaireCopy
                    {
                        QuestionnaireId = questionnaireId
                    }
                });
            return response;
        }






        public async Task<RespondentResponsesResponse> ZiskejCsv(int questionnaireId)
        {
            var apiForm = Configuration["url"];
            var response = await _httlClient.PostJsonAsync<RespondentResponsesResponse>(
                apiForm + "api/Respondent/Responses",
                new RespondentResponsesRequest
                {
                    Questionnaire =  new RespondentResponsesRequest.QuestionnaireResponses
                    {
                        QuestionnaireId = questionnaireId
                    }
                });

            return response;
        }












    }
}

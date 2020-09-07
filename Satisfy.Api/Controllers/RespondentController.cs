using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Satisfy.Api.Models;
using Satisfy.Shared;
using Satisfy.Shared.Form;
using Satisfy.Shared.Respondent;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Satisfy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespondentController : ControllerBase
    {
        [HttpPost("ResponseAdd")]

        public RespondentResponseResponse SaveResponse([FromBody]RespondentResponseRequest request)
        {
            var response = new Response()
            {
                AnswerId = request.AnswerId,
                QuestionId = request.QuestionId,
                QuestionnaireId = request.QuestionnaireId,
                RespondentId = request.RespondentId,
                TextOfRespondent = request.TextOfRespondent
            };
            using var db = new DbModel();
            db.Responses.Add(response);
            db.SaveChanges();

            return new RespondentResponseResponse()
            {
                Response = "Response Saved",
            };
        }


        [HttpPost("RespondentAdd")]
        public RespondentAddResponse RespondentAdd([FromBody]RespondentAddRequest request)
        {
            using var db = new DbModel();
            Respondent respondent1 = new Respondent()
            {
                ContactId = request.ContactId,
                QuestionnaireId = request.QuestionnaireId
            };
            db.Respondents.Add(respondent1);
            db.SaveChanges();

            var respondent = db.Respondents
                .FirstOrDefault(b => b.RespondentId == respondent1.RespondentId &&
                                     b.QuestionnaireId == respondent1.QuestionnaireId);
            if (respondent != null)
            {
                return new RespondentAddResponse()
                {
                    Response = "Respondent Added",
                    RespondentID = respondent.RespondentId
                };
            }
            else
            {
                return new RespondentAddResponse()
                {
                    Response = "Respondent not added",
                    RespondentID = 0
                };
            }
        }


        [HttpPost("QuestionnaireId")]
        public QuestionnaireIDResponse QuestionnaireId([FromBody]QuestionnaireIDRequest request)
        {
            using var db = new DbModel();
            var respondent = db.Respondents
                .FirstOrDefault(b => b.RespondentId == request.RespondentID);


            if (respondent != null)
            {
                return new QuestionnaireIDResponse()
                {
                    DotaznikID = respondent.QuestionnaireId
                };
            }
            else
            {
                return new QuestionnaireIDResponse()
                {
                    DotaznikID = 0
                };
            }

            
        }


        [HttpPost("Responded")]
        public RespondentRespondedResponse Responded([FromBody]RespondentRespondedRequest request)
        {
            using var db = new DbModel();
            var responses = db.Responses
                .Where(b => b.RespondentId == request.RespondentID);
            if (responses.Any())
            {
                return new RespondentRespondedResponse()
                {
                    Responded = true
                };
            }
            return new RespondentRespondedResponse()
            {
                Responded = false
            };
        }



        [HttpPost("Responses")]
        public ActionResult<RespondentResponsesResponse> Responses([FromBody] RespondentResponsesRequest request)
        {
            using var db = new DbModel();
            var respondents = db.Respondents
                .Where(r => r.QuestionnaireId == request.Questionnaire.QuestionnaireId)
                .ToList();

            var csv = new RespondentResponsesResponse();

            foreach (var respondent in respondents)
            {
                List<string> data;
                var respondentCredentials = db.Contacts
                    .SingleOrDefault(c => c.ContactId == respondent.ContactId);
                if (respondentCredentials == null)
                {
                    data = new List<string>()
                    {
                        "Name",
                        "Surname",
                        "Company Name",
                        "Email"
                    };
                }
                else
                {
                    data = new List<string>()
                    {
                        respondentCredentials.Name,
                        respondentCredentials.Surname,
                        respondentCredentials.Company,
                        respondentCredentials.Email
                    };
                }

                foreach (var question in db.Questions.Where(q => q.QuestionnaireId == respondent.QuestionnaireId).Select(q => new Question
                {
                    QuestionId=q.QuestionId,
                }).ToArray())
                {
                    var responses = db.Responses.Where(r => r.QuestionId == question.QuestionId && r.RespondentId == respondent.RespondentId).Select(r =>new Response
                    {
                        TextOfRespondent = r.TextOfRespondent,
                        Answer = db.Answers.SingleOrDefault(a => a.AnswerId == r.AnswerId)
                    }).ToList();

                    if (responses.Any())
                    {
                        var responseString = new List<string>();
                        foreach (var response in responses)
                        {
                            if (response.Answer.Type == 0)
                            {
                                responseString.Add(response.TextOfRespondent);
                            }
                            else
                            {
                                responseString.Add(response.Answer.Text);
                            }
                        }
                        data.Add(string.Join("| ",responseString));
                    }
                    else
                    {
                        data.Add("not answered");
                    }
                }
                csv.Responses.CsvList.Add(data);
            }
            return Ok(csv);
        }













    }
}
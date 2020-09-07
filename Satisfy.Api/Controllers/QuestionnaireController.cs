using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Satisfy.Api.Models;
using Satisfy.Shared;
using Satisfy.Shared.Form;

namespace Satisfy.Api.Controllers
{
    //this controller is for handling manipulation with Questionnaires
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        //this endpoint is used for getting list of Questionnaires for specific user ID
        [HttpPost("List")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<QuestionnaireListResponse> Post([FromBody] QuestionnaireListRequest value)
        {
            //Instantiate database 
            using var db = new DbModel();
            //Creation of response object 
            var result = new QuestionnaireListResponse
            {
                //Select to database where Questionnaires do have required UserId
                Questionnair = db.Questionnaires
                    .Where(d => d.UserId == value.Questionnaire.UserId)
                    .Select(d => new QuestionnaireListResponse.QuestionnaireList
                    {
                        //Filling up response object with data from database
                        EndDate = d.EndDate,
                        IsEvaluated = d.IsEvaluated,
                        IsPublished = d.IsPublished,
                        Name = d.Name,
                        QuestionnaireId = d.QuestionnaireId,
                        StartDate = d.StartDate,
                        UserId = d.UserId
                    })
                    .ToList()
            };
            //If there are no values to return
            if (result.Questionnair.Count == 0)
            {
                result = new QuestionnaireListResponse
                {
                    Questionnair = new List<QuestionnaireListResponse.QuestionnaireList>()
                };
            }
            return Ok(result);
        }

        //This endpoint is used for updating status of Questionnaire to status published
        [HttpPost("Publish")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<QuestionnairePublishResponse> Publish([FromBody] QuestionnairePublishRequest value)
        {
            //Instantiate database 
            using var db = new DbModel();
            //Select to database where Questionnaire is having required QuestionnaireId
            var singleOrDefault = db.Questionnaires
                .SingleOrDefault(d => d.QuestionnaireId == value.Questionnaire.QuestionnaireId);
            //If there is something to update than update status
            if (singleOrDefault == null) return BadRequest("Wrong ID");
            //Setting status of Questionnaire to published state
            singleOrDefault.IsPublished = true;
            //Updating state of Questionnaire in database
            db.Update(singleOrDefault);
            //Saving changes in database
            db.SaveChanges();

            var result = new QuestionnairePublishResponse
            {
                Questionnaire = new QuestionnairePublishResponse.QuestionnairePublish
                {
                    status = "Questionnaire was Published successfully"
                }
            };
            return Ok(result);
        }

        //This endpoint is used for getting Questionnaire from database by its QuestionnaireId
        [HttpPost("ById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<QuestionnaireGetResponse> ById([FromBody] QuestionnaireGetRequest value)
        {
            //Instantiate database 
            using var db = new DbModel();
            //Select to database where Questionnaire is having required QuestionnaireId
            var result = db.Questionnaires
                .Where(d => d.QuestionnaireId == value.Questionnaire.QuestionnaireId)
                .Select(d => new QuestionnaireGetResponse
                    {
                        Questionnaire = new QuestionnaireGetResponse.QuestionnaireGet
                        {
                            //Filling up response object with data from database
                            QuestionnaireId = d.QuestionnaireId,
                            Name = d.Name,
                            IsPublished = d.IsPublished,
                            IsEvaluated = d.IsEvaluated,
                            StartDate = d.StartDate,
                            EndDate = d.EndDate,
                            UserId = d.UserId,
                            Question = d.QuestionList
                                .Select(q => new QuestionnaireGetResponse.QuestionnaireGet.QuestionnaireGetQuestion
                                {
                                    //Filling up response object with Questions from database
                                    QuestionId = q.QuestionId,
                                    QuestionPosition = q.QuestionPosition,
                                    Text = q.Text,
                                    Answer = q.AnswerList
                                        .Select(a =>
                                            new QuestionnaireGetResponse.QuestionnaireGet.QuestionnaireGetQuestion.
                                                QuestionnaireGetQuestionAnswer
                                                {
                                                    //Filling up response object with Answers from database
                                                    AnswerId = a.AnswerId,
                                                    AnswerPosition = a.AnswerPosition,
                                                    Text = a.Text,
                                                    Type = a.Type
                                                })
                                        .ToList()
                                })
                                .ToList()
                        }
                    }
                )
                .SingleOrDefault();
            //If there is no Questionnaire to return throw error
            if (result == null) return BadRequest("Questionnaire does not exist");
            //Success
            return Ok(result);
        }

        //This endpoint is for saving new Questionnaire
        [HttpPost("Save")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<QuestionnaireSaveResponse> Save([FromBody] QuestionnaireSaveRequest value)
        {
            //Instantiate database 
            using var db = new DbModel();
            //Creating new database object
            var questionnaire = new Questionnaire
            {
                //Filling up database object with data from response object
                EndDate = value.Questionnaire.EndDate,
                IsEvaluated = value.Questionnaire.IsEvaluated,
                IsPublished = value.Questionnaire.IsPublished,
                Name = value.Questionnaire.Name,
                StartDate = value.Questionnaire.StartDate,
                UserId = value.Questionnaire.UserId,
                QuestionList = value.Questionnaire.Question
                    .Select(q => new Question
                    {
                        //Filling up database object with data from response object Question
                        Text = q.Text,
                        QuestionPosition = q.QuestionPosition,
                        AnswerList = q.Answer
                            .Select(a => new Answer
                            {
                                //Filling up database object with data from response object Answer
                                AnswerPosition = a.AnswerPosition,
                                Text = a.Text,
                                Type = a.Type
                            })
                            .ToList()
                    })
                    .ToList()
            };
            //Adding new Questionnaire to database
            db.Questionnaires.Add(questionnaire);
            //Saving changes in database
            db.SaveChanges();
            return Ok(new QuestionnaireSaveResponse
            {
                Questionnaire = new QuestionnaireSaveResponse.QuestionnairePublish
                {
                    Success = true
                }
            });
        }


        //This endpoint is for updating new Questionnaire
        [HttpPost("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<QuestionnaireUpdateResponse> Update([FromBody] QuestionnaireUpdateRequest value)
        {
            //Instantiate database 
            using var db = new DbModel();
            //Select to database where Questionnaire is having required QuestionnaireId
            var result = db.Questionnaires
                .SingleOrDefault(d => d.QuestionnaireId == value.Questionnaire.QuestionnaireId);
            //If there is no Questionnaire to update
            if (result == null) return BadRequest("Questionnaire to update was not found");
            //Selecting Questions for Questionnaire
            var queryable = db.Questions.Where(q => q.QuestionnaireId == value.Questionnaire.QuestionnaireId);
            //Deleting old Questions from Questionnaire
            db.Questions.RemoveRange(queryable);
            //Saving changes to database
            db.SaveChanges();
            //Updating values of new Questionnaire
            result.EndDate = value.Questionnaire.EndDate;
            result.IsEvaluated = value.Questionnaire.IsEvaluated;
            result.IsPublished = value.Questionnaire.IsPublished;
            result.Name = value.Questionnaire.Name;
            result.StartDate = value.Questionnaire.StartDate;
            result.UserId = value.Questionnaire.UserId;
            result.QuestionList = value.Questionnaire.Question
                .Select(q => new Question
                {
                    QuestionPosition = q.QuestionPosition,
                    Text = q.Text,
                    AnswerList = q.Answer
                        .Select(a => new Answer
                        {
                            AnswerPosition = a.AnswerPosition,
                            Text = a.Text,
                            Type = a.Type
                        })
                        .ToList()
                })
                .ToList();

            //Update of Questionnaire 
            db.Update(result);
            //Saving changes in database
            db.SaveChanges();
            //Success
            return Ok(new QuestionnaireSaveResponse
            {
                Questionnaire = new QuestionnaireSaveResponse.QuestionnairePublish
                {
                    Success = true
                }
            });
        }

        //This endpoint is for creating copy of questionnaire
        [HttpPost("Copy")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<QuestionnaireCopyResponse> Delete([FromBody] QuestionnaireCopyRequest value)
        {
            //Instantiate database 
            using var db = new DbModel();
            //Select to database where Questionnaire is having required QuestionnaireId
            var result = db.Questionnaires
                .Where(d => d.QuestionnaireId == value.Questionnaire.QuestionnaireId)
                .Select(d => new Questionnaire
                {
                    //Filling up new Questionnaire with data from database
                    Name = d.Name + "_copy",
                    IsPublished = false,
                    IsEvaluated = d.IsEvaluated,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    UserId = d.UserId,
                    QuestionnaireId = 0,
                    QuestionList = d.QuestionList
                            .Select(q => new Question
                            {
                                //Filling up new Questionnaire with Questions from database
                                QuestionPosition = q.QuestionPosition,
                                Text = q.Text,
                                AnswerList = q.AnswerList
                                    .Select(a => new Answer
                                    {
                                        //Filling up new Questionnaire with Answers from database
                                        AnswerPosition = a.AnswerPosition,
                                        Text = a.Text,
                                        Type = a.Type
                                    })
                                    .ToList()
                            })
                            .ToList()

                }
                )
                .SingleOrDefault();
            //If there was no Questionnaire with required QuestionnaireId throw error
            if (result == null) return BadRequest("Questionnaire does not exist");
            //Adding copy of Questionnaire to database
            db.Questionnaires.Add(result);
            //Saving changes in database
            db.SaveChanges();
            //Success
            return Ok(new QuestionnaireDeleteResponse
            {
                Questionnaire = new QuestionnaireDeleteResponse.QuestionnaireDelete
                {
                    Success = true
                }
            });

        }

    }
}

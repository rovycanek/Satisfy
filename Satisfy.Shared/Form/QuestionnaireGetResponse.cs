using System;
using System.Collections.Generic;

namespace Satisfy.Shared.Form
{
    public class QuestionnaireGetResponse
    {
        public QuestionnaireGet Questionnaire { get; set; } = new QuestionnaireGet();
        public class QuestionnaireGet
        {
            public int QuestionnaireId { get; set; }
            public string Name { get; set; }
            public bool IsPublished { get; set; }
            public bool IsEvaluated { get; set; }
            public int UserId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public List<QuestionnaireGetQuestion> Question { get; set; } = new List<QuestionnaireGetQuestion>();
            public class QuestionnaireGetQuestion
            {
                public int QuestionId { get; set; }
                public string Text { get; set; }
                public int QuestionPosition { get; set; }
                public List<QuestionnaireGetQuestionAnswer> Answer { get; set; } = new List<QuestionnaireGetQuestionAnswer>();
                public class QuestionnaireGetQuestionAnswer
                {
                    public int AnswerId { get; set; }
                    public string Text { get; set; }
                    public int AnswerPosition { get; set; }
                    public int Type { get; set; }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace Satisfy.Shared.Form
{
    public class QuestionnaireSaveRequest
    {
        public QuestionnaireSave Questionnaire { get; set; } = new QuestionnaireSave();
        public class QuestionnaireSave
        {
            public int QuestionnaireId { get; set; }
            public string Name { get; set; }
            public bool IsPublished { get; set; }
            public bool IsEvaluated { get; set; }
            public int UserId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public List<QuestionnaireQuestion> Question { get; set; } = new List<QuestionnaireQuestion>();
            public class QuestionnaireQuestion
            {
                public string Text { get; set; }
                public int QuestionPosition { get; set; }
                public List<QuestionnaireQuestionAnswer> Answer { get; set; } = new List<QuestionnaireQuestionAnswer>();
                public class QuestionnaireQuestionAnswer
                {
                    public string Text { get; set; }
                    public int AnswerPosition { get; set; }
                    public int Type { get; set; }
                }
            }
        }
    }
}
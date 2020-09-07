using System;
using System.Collections.Generic;

namespace Satisfy.Shared.Form
{
    public class QuestionnaireListResponse
    {
        public List<QuestionnaireList> Questionnair { get; set; } = new List<QuestionnaireList>();
        public class QuestionnaireList
        {
            public int QuestionnaireId { get; set; }
            public bool IsPublished { get; set; }
            public bool IsEvaluated { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
        }
    }
}
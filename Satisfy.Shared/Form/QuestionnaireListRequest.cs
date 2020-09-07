using System;
using System.Collections.Generic;

namespace Satisfy.Shared.Form
{
    public class QuestionnaireListRequest
    {
        public QuestionnaireList Questionnaire { get; set; } = new QuestionnaireList();
        public class QuestionnaireList
        {
            public int UserId { get; set; }
        }
    }
}
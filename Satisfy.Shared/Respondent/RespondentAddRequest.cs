using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared
{
    public class RespondentAddRequest
    {
        public int QuestionnaireId { get; set; }
        public int ContactId { get; set; }

        public RespondentAddRequest(int questionnaireId, int contactId)
        {
            QuestionnaireId = questionnaireId;
            ContactId = contactId;
        }
        public RespondentAddRequest() { }
        
    }
}

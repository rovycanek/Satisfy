using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared.Respondent
{
    public class RespondentRespondedRequest
    {
        public int RespondentID { get; set; }

        public RespondentRespondedRequest(int respondentID)
        {
            RespondentID = respondentID;
        }

        public RespondentRespondedRequest() { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared.Contact
{
    public class ContactListNoRespondedRequest
    {
        public int UserID { get; set; }
        public int QuestionaireID { get; set; }

        public ContactListNoRespondedRequest(int userID, int questionaireID)
        {
            UserID = userID;
            QuestionaireID = questionaireID;
        }
        public ContactListNoRespondedRequest() { }
    }
}

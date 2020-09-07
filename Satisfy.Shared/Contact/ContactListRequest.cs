using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared
{
    public class ContactListRequest
    {
        public int UserID { get; set; }

        public ContactListRequest(int userID)
        {
            UserID = userID;
        }
        public ContactListRequest() { }

    }
}

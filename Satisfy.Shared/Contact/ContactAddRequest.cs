using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared
{
    public class ContactAddRequest
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public ContactAddRequest(int userID,string name, string email, string surname, string company)
        {
            UserID = userID;
            Name = name;
            Email = email;
            Surname = surname;
            Company = company;
        }
        public ContactAddRequest() { }
    }
}

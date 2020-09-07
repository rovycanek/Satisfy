using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared
{
    public class ContactListResponse
    {
        public List<Contact> List { get; set; }

        public class Contact
        {
            public int ContactId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public int UserId { get; set; }
            public string Surname { get; set; }
            public string Company { get; set; }

            public Contact(string name, string email, int userID, string surname, string company)
            {
                Name = name;
                Email = email;
                UserId = userID;
                Surname = surname;
                Company = company;
            }
            public Contact() { }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Satisfy.Shared;
using Satisfy.Shared.Contact;
using System.Linq;
using Satisfy.Api.Models;
using System.Collections.Generic;

namespace Satisfy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        //This endpoint is for adding new Contact
        [HttpPost("Add")]
        public ContactAddResponse ContactAdd([FromBody]ContactAddRequest request)
        {
            //Instantiate database 
            using var db = new DbModel();
            //empty field check
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email))
                return new ContactAddResponse {Response = "Any field can't be empty"};
            //Adding request to database
            db.Contacts.Add(new Contact {
                //Converting Request body to Contact
                Email = request.Email, 
                Name = request.Name, 
                UserId = request.UserID,
                Surname = request.Surname,
                Company = request.Company
            });
            //Saving changes in database
            db.SaveChanges();
            //Success
            return new ContactAddResponse {Response = "Contact Added"};
        }
        //this endpoint is used for getting list of Contacts for specific user ID
        [HttpPost("ContactList")]
        public ContactListResponse ContactList([FromBody]ContactListRequest request)
        {
            //Instantiate database 
            using var db = new DbModel();
            //Select to database where Contacts do have required UserId
            var list = db.Contacts.Where(b => b.UserId == request.UserID)
                .Select(c => new ContactListResponse.Contact
                {
                    ContactId = c.ContactId,
                    Email = c.Email,
                    Name = c.Name,
                    UserId = c.UserId,
                    Surname = c.Surname,
                    Company = c.Company

                })
                .ToList();

            return new ContactListResponse()
            {
                List = list
            };
        }

        [HttpPost("ContactListNoResponded")]
        public ContactListResponse ContactListNoResponded([FromBody]ContactListNoRespondedRequest request)
        {
            //Instantiate database 
            using var db = new DbModel();
            var list = db.Contacts.Where(b => b.UserId == request.UserID)
                .Select(c => new ContactListResponse.Contact
                {
                    ContactId = c.ContactId,
                    Email = c.Email,
                    Name = c.Name,
                    UserId = c.UserId,
                    Surname = c.Surname,
                    Company = c.Company

                })
                .ToList();


            var respondents = db.Respondents.Where(b => b.QuestionnaireId == request.QuestionaireID).ToList();

            for(var i=0; i < respondents.Count(); i++)
            {
                var responses = db.Responses.Where(b => b.QuestionnaireId == request.QuestionaireID && b.RespondentId==respondents[i].RespondentId).ToList();
                if (responses.Any())
                {
                    var listpart = db.Contacts.Where(b => b.UserId == request.UserID && b.ContactId == respondents[i].ContactId)
                        .Select(c => new ContactListResponse.Contact
                        {
                            ContactId = c.ContactId,
                            Email = c.Email,
                            Name = c.Name,
                            UserId = c.UserId,
                            Surname = c.Surname,
                            Company = c.Company

                        })
                        .ToList();
                    list = list.Except(listpart, new IdComparer()).ToList();
                    
                }
            }

            return new ContactListResponse()
            {
                List = list
            };
        }

    }

    public class IdComparer : IEqualityComparer<ContactListResponse.Contact>
    {
        public int GetHashCode(ContactListResponse.Contact co)
        {
            if (co == null)
            {
                return 0;
            }
            return co.ContactId.GetHashCode();
        }

        public bool Equals(ContactListResponse.Contact x1, ContactListResponse.Contact x2)
        {
            if (object.ReferenceEquals(x1, x2))
            {
                return true;
            }
            if (object.ReferenceEquals(x1, null) ||
                object.ReferenceEquals(x2, null))
            {
                return false;
            }
            return x1.ContactId == x2.ContactId;
        }
    }
}
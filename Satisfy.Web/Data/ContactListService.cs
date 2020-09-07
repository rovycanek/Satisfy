using Microsoft.AspNetCore.Components;
using Satisfy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Satisfy.Shared.Contact;

namespace Satisfy.Web.Data
{
    public class ContactListService
    {
        private HttpClient _httlClient;
        private readonly IConfiguration Configuration;

        public ContactListService(HttpClient httpClient, IConfiguration configuration)
        {
            _httlClient = httpClient;
            Configuration = configuration;
        }

        public async Task<ContactListResponse> Contacts(int userID)
        {
            var ContactList = Configuration["url"];
            ContactListResponse response = await _httlClient.PostJsonAsync<ContactListResponse>(ContactList+ "api/Contact/ContactList", new ContactListRequest(userID));
            return response;
        }

        public async Task<ContactAddResponse> AddContact(int userID, string name, string email, string surname, string company)
        {
            var ContactAdd = Configuration["url"];
            ContactAddResponse response = await _httlClient.PostJsonAsync<ContactAddResponse>(ContactAdd+ "api/Contact/Add", new ContactAddRequest(userID,name,email,surname,company));
            return response;
        }

        public async Task<ContactListResponse> NoRespondedContacts(int userID, int questionaireID)
        {
            var ContactList = Configuration["url"];
            ContactListResponse response = await _httlClient.PostJsonAsync<ContactListResponse>(ContactList + "api/Contact/ContactListNoResponded", new ContactListNoRespondedRequest(userID,questionaireID));
            return response;
        }

        public string URlConfig()
        {
            string url = Configuration["url"];
            return url;
        }
    }
}

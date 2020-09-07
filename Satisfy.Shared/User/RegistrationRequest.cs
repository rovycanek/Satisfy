using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared
{
    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        public RegistrationRequest(string username, string name, string email, string password, string repeatPassword)
        {
            Username = username;
            Name = name;
            Email = email;
            Password = password;
            RepeatPassword = repeatPassword;
        }
        public RegistrationRequest()
        {
        }
    }
}

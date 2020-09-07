using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Satisfy.Shared;
using User = Satisfy.Api.Models.User;

namespace Satisfy.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class Login2Controller : ControllerBase
    {
        // POST: api/User/Register
        [HttpPost("Register")]
        public RegistrationResponse Register([FromBody]RegistrationRequest request)
        {
            using (var db = new DbModel())
            {
                //kontrola prázdného pole
                if (request.Username == null || request.Name == null || request.Password == null || request.Email == null)
                {
                    return new RegistrationResponse()
                    {
                        Response = "Any field can't be empty"
                    };
                }
                //kontrola only white-space characters
                if (request.Username.Trim() == "" || request.Name.Trim() == "" || request.Password.Trim() == "" || request.Email.Trim() == "")
                {
                    return new RegistrationResponse()
                    {
                        Response = "Every field must have non white-space character"
                    };
                }


                //kontrola, jestli se shodují hesla
                if (request.Password != request.RepeatPassword)
                {
                    return new RegistrationResponse()
                    {
                        Response = "Passwords not match"
                    };
                }

                //kontrola validniho emailu
                if (!(new EmailAddressAttribute().IsValid(request.Email.Trim())))
                {
                    return new RegistrationResponse()
                    {
                        Response = "Email " + request.Email.Trim() + " has incorrect form"
                    };
                }

                //kontrola, jestli je username volne
                var users = db.Users.Where(b => b.Username == request.Username.Trim());
                if (users.Count() != 0)
                {
                    return new RegistrationResponse()
                    {
                        Response = "Username " + request.Username.Trim() + " is already used"
                    };
                }

                var passwordHash = Hash.GetHashString(request.Password.Trim());
                db.Users.Add(new User()
                {
                    Email = request.Email.Trim(),
                    Name = request.Name.Trim(),
                    Password = passwordHash,
                    Username = request.Username.Trim()
                });
                db.SaveChanges();
                return new RegistrationResponse()
                {
                    Response = "Account " + request.Username + " has been created"
                };
            }
        }


        // POST: api/User/Login
        [HttpPost("Login")]
        public LoginResponse Login([FromBody]LoginRequest request)
        {
            using var db = new DbModel();
            var users = db.Users.FirstOrDefault(b => b.Username == request.Username);


            if (users!=null)
            {
                var passwordHash = Hash.GetHashString(request.Password);
                if (users.Password == passwordHash)
                {
                    return new LoginResponse()
                    {
                        Token = "Token123456789" + users.Email,
                        TokenValidTo = DateTime.Now,
                        Success = true,
                        ErrMsg = "logged in",
                        UserID = users.UserId,
                        Name = users.Name,
                        Username = users.Username,
                        Email = users.Email
                    };
                }
            }

            return new LoginResponse()
            {
                Token = "",
                TokenValidTo = DateTime.Now,
                Success = false,
                ErrMsg = "wrong user name or password"
            };
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Satisfy.Shared;
using System.Net;
using System.Net.Mail;

namespace Satisfy.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost("SendMail")]

        public SendMailResponse SendMail([FromBody]SendMailRequest request)
        {
            var message = new MailMessage();
            message.To.Add(request.TO);
            message.Subject = request.Subject;
            message.Body = request.Body;
            message.From = new MailAddress("a0vtv.Satisfy@gmail.com");
            message.IsBodyHtml = false;
            var smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("a0vtv.Satisfy@gmail.com", "s4t1sfyaovtv");
            smtp.Send(message);

            return new SendMailResponse()
            {
                Response = "Contact Added"
            };

        }
    }
}

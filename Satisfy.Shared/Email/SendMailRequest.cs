using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared
{
    public class SendMailRequest
    {
        public string TO { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public SendMailRequest(string to, string subject,string body)
        {
            TO = to;
            Subject = subject;
            Body = body;
        }
        public SendMailRequest() { }
    }
}

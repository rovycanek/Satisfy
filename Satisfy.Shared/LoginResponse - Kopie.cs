using System;
using System.Collections.Generic;
using System.Text;

namespace Satisfy.Shared
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime TokenValidTo { get; set; }
        public bool Success { get; set; }
        public string ErrMsg { get; set; }
    }
}

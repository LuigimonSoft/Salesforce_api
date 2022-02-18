using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfAuthenticate.Models
{
    public class UserAuthenticate
    { 
        public string access_token { get; set; }
        public string instance_url { get; set; }
        public string id { get; set; }

        public sfAuthenticate.TokenTypes TokenType { get; set; }
        public string token_type { get; set; }
        public string issued_at { get; set; } 
        public string signature { get; set; }

        public string error { get; set; }
        public string error_description { get; set; }
    }
}

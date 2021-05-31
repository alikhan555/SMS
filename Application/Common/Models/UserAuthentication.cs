using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class UserAuthentication
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string JWToken { get; set; }
    }
}
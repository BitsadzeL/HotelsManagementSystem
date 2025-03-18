using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Models.Dtos.Identity
{
    public class AdminRegistrationDto
    {
        public string Email { get; set; }  // Identity Username
        public string Password { get; set; }  // Identity Password
    }

}

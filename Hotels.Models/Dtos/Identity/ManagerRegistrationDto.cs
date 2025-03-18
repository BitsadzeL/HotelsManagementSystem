using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Models.Dtos.Identity
{
    public class ManagerRegistrationDto
    {

        public string Password { get; set; }  // Identity Password
        public string Email { get; set; }  

        // Additional Manager Information
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }  // Must be 11 characters, numeric
        public string PhoneNumber { get; set; }

        // Hotel Information (Hotel ID is required)
        public int HotelId { get; set; }
    }

}

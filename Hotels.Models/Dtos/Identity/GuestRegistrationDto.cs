namespace Hotels.Models.Dtos.Identity
{
    public class GuestRegistrationDto
    {
        public string Password { get; set; }  // Identity Password
        public string Email { get; set; }  // Identity Email

        // Additional Guest Information
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }  // Must be 11 characters, numeric
        public string PhoneNumber { get; set; }
    }


}

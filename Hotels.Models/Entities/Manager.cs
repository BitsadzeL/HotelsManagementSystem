namespace Hotels.Models.Entities
{
    public class Manager
    {
        
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }


        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        //Manager manages 1 hotel 1:1
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
    }
}

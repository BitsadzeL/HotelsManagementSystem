namespace Hotels.Models.Dtos.Managers
{
    public class ManagerAddingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? HotelId { get; set; }
    }
}

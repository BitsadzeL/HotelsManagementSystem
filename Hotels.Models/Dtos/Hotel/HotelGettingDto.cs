using Hotels.Models.Dtos.Manager;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.Dtos.Hotel
{
    public class HotelGettingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Range(1, 5)]
        public float Rating { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        
        public ManagerGettingDto Manager { get; set; }


        
        //public List<Room> Rooms { get; set; }
    }
}

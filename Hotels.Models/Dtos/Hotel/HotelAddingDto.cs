using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.Dtos.Hotel
{
    public class HotelAddingDto
    {
        public string Title { get; set; }

        [Range(1, 5)]
        public float Rating { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}

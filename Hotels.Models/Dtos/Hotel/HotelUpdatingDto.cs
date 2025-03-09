using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.Dtos.Hotel
{
    public class HotelUpdatingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }


        [Range(1, 5)]
        public float Rating { get; set; }
    }
}

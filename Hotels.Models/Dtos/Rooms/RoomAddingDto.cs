using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.Dtos.Rooms
{
    public class RoomAddingDto
    {
        
        public string Title { get; set; }
        public bool? IsFree { get; set; }
        public float Price { get; set; }
        public int HotelId { get; set; }
    }
}

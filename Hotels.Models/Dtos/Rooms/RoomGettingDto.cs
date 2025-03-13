namespace Hotels.Models.Dtos.Rooms
{
    public class RoomGettingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsFree { get; set; }
        public float Price { get; set; }
        public int HotelId {  get; set; }
    }
}

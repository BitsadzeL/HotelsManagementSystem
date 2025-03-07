namespace Hotels.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Title { get;set; }
        public bool IsFree { get;set; }
        public float Price { get; set; }


        //many rooms may belong to one hotel M:1
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }


    }
}

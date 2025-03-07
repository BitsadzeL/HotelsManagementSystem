using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Range(1,5)]
        public float Rating { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        //Hotel has one manager 1:1
        public Manager Manager { get; set; }


        //Hotel has many rooms 1:M
        public List<Room> Rooms { get; set; }



    }
}

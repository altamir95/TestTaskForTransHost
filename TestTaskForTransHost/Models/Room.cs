using System.Collections.Generic;

namespace TestTaskForTransHost.Models
{
    public class Room
    {
       
        public int Id { get; set; }
        public int RoomClassId { get; set; }
        public  RoomClass RoomClass { get; set; }
        public int HotelId { get; set; }
        public  Hotel Hotel { get; set; } 

        public  List<RoomReservation>  RoomReservations  { get; set; }
        public Room()
        {
            RoomReservations = new List<RoomReservation>();
        }

    }
}

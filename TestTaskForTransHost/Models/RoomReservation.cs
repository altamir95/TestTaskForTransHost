using System;

namespace TestTaskForTransHost.Models
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }

        public int RoomId { get; set; }
        public  Room  Room { get; set; }

        public int ClientId { get; internal set; }
        public  Client Client { get; set; }

    }
}

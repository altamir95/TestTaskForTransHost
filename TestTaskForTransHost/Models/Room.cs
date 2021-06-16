﻿namespace TestTaskForTransHost.Models
{
    public class Room
    {
       
        public int Id { get; set; }
        public int RoomClassId { get; set; }
        public virtual RoomClass RoomClass { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

    }
}

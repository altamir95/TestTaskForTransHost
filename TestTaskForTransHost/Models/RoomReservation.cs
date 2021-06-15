﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForTransHost.Models
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }

        public int RoomId { get; set; }
        public virtual Room  Room { get; set; }

        public int ClientId { get; internal set; }
        public virtual Client Client { get; set; }

    }
}

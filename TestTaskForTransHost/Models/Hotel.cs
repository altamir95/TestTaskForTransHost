using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestTaskForTransHost.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public Hotel()
        {
            Rooms = new List<Room>();
        }
    }
}

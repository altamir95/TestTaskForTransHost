using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskForTransHost.Enums;
using TestTaskForTransHost.Models;

namespace TestTaskForTransHost.ViewModels
{
    public class ReserveRoomViewModel
    {

        public Room Room { get; set; } 
        public string ClientPassportNumber { get; set; }
        public string ClientName { get; set; }
        public DateTime ClientDateBirth { get; set; }
         
    }
}

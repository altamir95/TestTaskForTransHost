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

        public int roomId { get; set; }
        public string ClientPassportNumber { get; set; }

        public DateTime ClientDateBirth { get; set; }

        public string ClientFullName { get; set; }

    }
}

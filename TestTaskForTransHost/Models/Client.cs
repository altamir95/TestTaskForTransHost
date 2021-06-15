using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForTransHost.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string PassportNumber { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

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

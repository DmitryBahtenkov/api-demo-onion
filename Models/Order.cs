using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
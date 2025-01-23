using Humanizer.Localisation;
using GBook.Models;
using System.ComponentModel.DataAnnotations;

namespace GBook.Models
{
    public class Messages
    {   
        public int Id { get; set; }
        public required string Message { get; set; }
        public DateTime MessageDateTime { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; }
        
    }
}

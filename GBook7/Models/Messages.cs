using System.ComponentModel.DataAnnotations;

namespace GBook.Models
{
    public class Messages
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "Message cannot be empty")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 500 characters")]
        [Display(Name = "Message")]
        public required string Message { get; set; }

        public DateTime MessageDateTime { get; set; }

        public int UserId { get; set; }

        public Users? User { get; set; }
    }
}

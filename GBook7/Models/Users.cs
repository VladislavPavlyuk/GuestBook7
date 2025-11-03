using System.ComponentModel.DataAnnotations;

namespace GBook.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login must be between 3 and 50 characters")]
        [Display(Name = "Login")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        public string? Salt { get; set; }

        public Users()
        {
            this.Messages = new HashSet<Messages>();
        }

        public ICollection<Messages>? Messages { get; set; }
    }
}
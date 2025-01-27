using System.ComponentModel.DataAnnotations;

namespace WebsiteASPNETCOREMVC.Models
{
    public class ContactView
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }


        public string Message { get; set; }
    }
}

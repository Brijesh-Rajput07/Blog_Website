using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteASPNETCOREMVC.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [NotMapped]
        public IFormFile FeaturedImage { get; set; }

        public string FilePath { get; set; }

        public DateTime PublishDate { get; set; }
    }
}

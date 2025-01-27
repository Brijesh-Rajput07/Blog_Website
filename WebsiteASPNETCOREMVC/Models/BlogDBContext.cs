using Microsoft.EntityFrameworkCore;

namespace WebsiteASPNETCOREMVC.Models
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
                
        }
        public DbSet<Blog> Blogs { get; set; }
    }
}

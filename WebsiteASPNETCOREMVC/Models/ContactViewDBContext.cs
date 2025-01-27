using Microsoft.EntityFrameworkCore;

namespace WebsiteASPNETCOREMVC.Models
{
    public class ContactViewDBContext : DbContext
    {
        public ContactViewDBContext(DbContextOptions<ContactViewDBContext> options) : base(options)
        {
                
        }
        public DbSet<ContactView> Contacts { get; set; }
    }
}

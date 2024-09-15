using Microsoft.EntityFrameworkCore;
using Product.Classes.Models;

namespace Product.Contexts
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Cart> UserCart { get; set; }
    }
}

using Dictionary.Models;
using Microsoft.EntityFrameworkCore;

namespace MetallAlloy.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Word> words { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
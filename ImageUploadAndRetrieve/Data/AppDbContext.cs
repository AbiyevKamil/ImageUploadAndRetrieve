using ImageUploadAndRetrieve.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageUploadAndRetrieve.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
    }
}

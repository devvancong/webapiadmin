using Microsoft.EntityFrameworkCore;
using WebEntryModel.EF;

namespace WebEntryModel
{
    public class AppDatabase : DbContext
    {
        public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
        {
        }

        public DbSet<Userview> Userviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
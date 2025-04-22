using AntiSpy.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace TiktokWidget.Business.Context
{
    public class AntiSpyDbContext : DbContext
    {
        public AntiSpyDbContext(DbContextOptions<AntiSpyDbContext> options) : base(options)
        {
        }

        public DbSet<StoreEntity> Store { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StoreEntity>().ToTable("Store");
        }
    }
}

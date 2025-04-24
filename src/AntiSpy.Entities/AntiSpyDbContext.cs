using Microsoft.EntityFrameworkCore;


public class AntiSpyDbContext : DbContext
{
    public AntiSpyDbContext(DbContextOptions<AntiSpyDbContext> options) : base(options)
    {
    }

    public DbSet<StoreEntity> Store { get; set; }
    public DbSet<AntiCopySettingsEntity> AntiCopySetting { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StoreEntity>().ToTable("Store");
        modelBuilder.Entity<StoreEntity>()
            .HasOne(s => s.AntiCopySettings)
            .WithOne(a => a.Store)
            .HasForeignKey<AntiCopySettingsEntity>(a => a.StoreId);
    }
}

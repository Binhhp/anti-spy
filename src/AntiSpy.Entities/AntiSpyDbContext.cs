using Microsoft.EntityFrameworkCore;


public class AntiSpyDbContext : DbContext
{
    public AntiSpyDbContext(DbContextOptions<AntiSpyDbContext> options) : base(options)
    {
    }

    public DbSet<StoreEntity> Store { get; set; }
    public DbSet<SettingsEntity> Settings { get; set; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new SettingsEntity.Configuration());
    }
    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedTime = DateTime.UtcNow;
                entry.Entity.ModifyTime = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifyTime = DateTime.UtcNow;
            }
        }
    }
}

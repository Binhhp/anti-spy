using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StoreEntity : BaseEntity
{
    public string InstanceId { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public string? SiteId { get; set; }
    public string? SiteUrl { get; set; }
    public string? SiteDisplayName { get; set; }
    public string? SiteDescription { get; set; }
    public string? AppInstanceName { get; set; }
    public string? BusinessName { get; set; }
    public string? Timezone { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string? Currency { get; set; }
    public SettingsEntity Settings { get; set; }
    public class Configuration : IEntityTypeConfiguration<StoreEntity>
    {
        public void Configure(EntityTypeBuilder<StoreEntity> builder)
        {
            builder.ToTable("Store");
            builder
                .HasOne(s => s.Settings)
                .WithOne(a => a.Store)
                .HasForeignKey<SettingsEntity>(a => a.StoreId);
        }
    }
}

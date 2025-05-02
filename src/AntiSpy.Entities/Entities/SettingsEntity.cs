
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SettingsEntity : BaseEntity
{
    // ==== Content Protection Settings ====
    public bool ProtectImages { get; set; }
    public bool ProtectText { get; set; }
    public bool StopKeyboardShortcuts { get; set; }
    public bool ShowAlertMessage { get; set; }
    public string SelectionDisabledMessage { get; set; }

    // ==== Alert Configuration ====
    public int DisappearAfterSeconds { get; set; }

    // ==== Legal Notice Settings ====
    public bool LogLegalNotice { get; set; }
    public string LegalHeader { get; set; }
    public string LegalFooter { get; set; }
    public string LegalContent { get; set; }
    public string StoreId { get; set; }
    public StoreEntity Store { get; set; }
    public class Configuration : IEntityTypeConfiguration<SettingsEntity>
    {
        public void Configure(EntityTypeBuilder<SettingsEntity> builder)
        {
            builder.ToTable("Settings");
        }
    }
}

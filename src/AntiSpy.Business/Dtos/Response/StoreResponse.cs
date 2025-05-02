public class StoreResponse
{
    public string Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime ModifyTime { get; set; }
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
    public string? Currency { get; set; }
    public SettingsResponse Settings { get; set; }
    public StoreResponse(StoreEntity storeEntity)
    {
        Id = storeEntity.Id;
        CreatedTime = storeEntity.CreatedTime;
        ModifyTime = storeEntity.ModifyTime;
        InstanceId = storeEntity.InstanceId;
        Email = storeEntity.Email;
        Phone = storeEntity.Phone;
        Country = storeEntity.Country;
        SiteId = storeEntity.SiteId;
        SiteUrl = storeEntity.SiteUrl;
        SiteDisplayName = storeEntity.SiteDisplayName;
        SiteDescription = storeEntity.SiteDescription;
        AppInstanceName = storeEntity.AppInstanceName;
        BusinessName = storeEntity.BusinessName;
        Timezone = storeEntity.Timezone;
        Currency = storeEntity.Currency;
        Settings = storeEntity.Settings != null ? new SettingsResponse(storeEntity.Settings) : null;
    }

    public class SettingsResponse
    {
        public bool ProtectImages { get; set; }
        public bool ProtectText { get; set; }
        public bool StopKeyboardShortcuts { get; set; }
        public bool ShowAlertMessage { get; set; }
        public string SelectionDisabledMessage { get; set; }
        public int DisappearAfterSeconds { get; set; }
        public bool LogLegalNotice { get; set; }
        public string LegalHeader { get; set; }
        public string LegalFooter { get; set; }
        public string LegalContent { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public SettingsResponse(SettingsEntity entity)
        {
            ProtectImages = entity.ProtectImages;
            ProtectText = entity.ProtectText;
            StopKeyboardShortcuts = entity.StopKeyboardShortcuts;
            ShowAlertMessage = entity.ShowAlertMessage;
            SelectionDisabledMessage = entity.SelectionDisabledMessage;
            DisappearAfterSeconds = entity.DisappearAfterSeconds;
            LogLegalNotice = entity.LogLegalNotice;
            LegalHeader = entity.LegalHeader;
            LegalFooter = entity.LegalFooter;
            LegalContent = entity.LegalContent;
            CreatedTime = entity.CreatedTime;
            ModifyTime = entity.ModifyTime;
        }
    }
}
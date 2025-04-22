using System.ComponentModel.DataAnnotations;

namespace AntiSpy.Entities.Entities
{
    public class StoreEntity
    {
        [Key]
        public string Id { get; set; }
        public string InstanceId { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }
        public string SiteId { get; set; }
        public string SiteUrl { get; set; }
        public string SiteDisplayName { get; set; }
        public string SiteDescription { get; set; }
        public string AppInstanceName { get; set; }
        public string? BusinessName { get; set; }
        public string? Timezone { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string? Currency { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public StoreEntity()
        {
            CreatedTime = DateTime.UtcNow;
            ModifyTime = DateTime.UtcNow;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TiktokWidget.Business.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AntiSpyDbContext>
    {
        private string Server = "Server=103.149.99.97;Database=WixTiktokTest;User Id=dev;Password=orichi123!@#;Encrypt=False;MultipleActiveResultSets=True;TrustServerCertificate=True;";
        private string Localhost = "Server=.;Database=ShopifyTiktok;User Id=sa;Password=binhhp20;Encrypt=False;MultipleActiveResultSets=True;TrustServerCertificate=True;";
        public AntiSpyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AntiSpyDbContext>();
            optionsBuilder.UseSqlServer(Localhost);
            return new AntiSpyDbContext(optionsBuilder.Options);
        }
    }
}

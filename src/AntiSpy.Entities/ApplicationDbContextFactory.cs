using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AntiSpyDbContext>
{
    private string Localhost = "Server=.;Database=ShopifyTiktok;User Id=sa;Password=binhhp20;Encrypt=False;MultipleActiveResultSets=True;TrustServerCertificate=True;";
    public AntiSpyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AntiSpyDbContext>();
        optionsBuilder.UseSqlServer(Localhost);
        return new AntiSpyDbContext(optionsBuilder.Options);
    }
}

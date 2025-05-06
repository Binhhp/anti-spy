using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AntiSpyDbContext>
{
    private string ServerTest = "Server=103.155.160.135;Database=AntiSpy-Test;User Id=dev;Password=orichi123!@#;Encrypt=False;MultipleActiveResultSets=True;TrustServerCertificate=True;";
    private string Localhost = "Server=.;Database=AntiSpy-Test;User Id=sa;Password=aod@123;Encrypt=False;MultipleActiveResultSets=True;TrustServerCertificate=True;";
    public AntiSpyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AntiSpyDbContext>();
        optionsBuilder.UseSqlServer(Localhost);
        return new AntiSpyDbContext(optionsBuilder.Options);
    }
}

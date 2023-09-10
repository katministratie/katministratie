using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Superkatten.Katministratie.Infrastructure.Persistance;

public class KatministratieContextFactory : IDesignTimeDbContextFactory<KatministratieContext>
{
    public KatministratieContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<KatministratieContext>();
        //TODO: connectionstring op een andere manier ophalen ?
        var connectionString = "Server=localhost;User ID=katministrator;Password=4143vk.;Database=mysqldb";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
        optionBuilder.UseMySql(connectionString, serverVersion);

        return new KatministratieContext(optionBuilder.Options);
    }
}

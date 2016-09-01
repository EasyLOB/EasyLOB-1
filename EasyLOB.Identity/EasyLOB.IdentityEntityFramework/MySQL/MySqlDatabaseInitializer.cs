using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

// ASP.NET Identity: Using MySQL Storage with an EntityFramework MySQL Provider (C#)
// http://www.asp.net/identity/overview/getting-started/aspnet-identity-using-mysql-storage-with-an-entityframework-mysql-provider
// References: System.Data

namespace EasyLOB.Identity
{
    public class MySqlDatabaseInitializer : IDatabaseInitializer<ApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
            if (!context.Database.Exists())
            {
                context.Database.Create();
            }
            else
            {
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'",
                  "aspnetusers"));

                if (migrationHistoryTableExists.FirstOrDefault() == 0)
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }

            IdentityConfigurator.DatabaseInitializer(context);
        }
    }
}
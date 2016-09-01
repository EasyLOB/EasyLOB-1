using EasyLOB.Persistence;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace EasyLOB.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Security")
        {
        }

        static ApplicationDbContext()
        {
            /*
            // Refer to <configuration><entityframework><contexts> section in Web.config or App.config
            string providerName = AdoNetHelper.GetProviderName("Security");
            switch (providerName)
            {
                case "MySql.Data.MySqlClient":
                    Database.SetInitializer(new MySqlDatabaseInitializer());
                    break;

                default:
                    Database.SetInitializer<ApplicationDbContext>(new DatabaseInitializer());
                    break;
            }
             */
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class DatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            IdentityConfigurator.DatabaseInitializer(context);

            base.Seed(context);
        }
    }
}
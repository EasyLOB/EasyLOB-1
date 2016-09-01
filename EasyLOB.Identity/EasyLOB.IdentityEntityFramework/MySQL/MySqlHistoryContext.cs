using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

// ASP.NET Identity: Using MySQL Storage with an EntityFramework MySQL Provider (C#)
// http://www.asp.net/identity/overview/getting-started/aspnet-identity-using-mysql-storage-with-an-entityframework-mysql-provider
// References: System.Data

namespace EasyLOB.Persistence
{
    public class MySqlHistoryContext : HistoryContext
    {
        public MySqlHistoryContext(DbConnection existingConnection, string defaultSchema)
            : base(existingConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().Property(x => x.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(x => x.ContextKey).HasMaxLength(200).IsRequired();
        }
    }
}
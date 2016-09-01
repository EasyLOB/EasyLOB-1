using System.Data.Entity;

// ASP.NET Identity: Using MySQL Storage with an EntityFramework MySQL Provider (C#)
// http://www.asp.net/identity/overview/getting-started/aspnet-identity-using-mysql-storage-with-an-entityframework-mysql-provider
// References: System.Data

namespace EasyLOB.Persistence
{
    public class MySqlDbConfiguration : DbConfiguration
    {
        public MySqlDbConfiguration()
        {
            SetHistoryContext("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }
    }
}
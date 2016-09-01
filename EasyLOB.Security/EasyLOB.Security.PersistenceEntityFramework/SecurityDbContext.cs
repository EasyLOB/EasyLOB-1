using EasyLOB.Security.Data;
using System.Data.Entity;

namespace EasyLOB.Security.Persistence
{
    public partial class SecurityDbContext : DbContext
    {
        #region Properties

        //public DbSet<ModuleInfo> ModulesInfo { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<ActivityRole> ActivityRoles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion Properties

        #region Methods

        static SecurityDbContext()
        {
            /*
            // Refer to <configuration><entityframework><contexts> section in Web.config or App.config
            //Database.SetInitializer<SecurityDbContext>(null);
            //Database.SetInitializer<SecurityDbContext>(new CreateDatabaseIfNotExists<SecurityDbContext>());
             */
        }

        public SecurityDbContext()
            : base("Name=Security")
        {
            Setup();
        }

        //public SecurityDbContext(string connectionString)
        //    : base(connectionString)
        //{
        //    Setup();
        //}

        //public SecurityDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
        //    : base(objectContext, dbContextOwnsObjectContext)
        //{
        //    Setup();
        //}

        //public SecurityDbContext(DbConnection connection)
        //    : base(connection, false)
        //{
        //    Setup();
        //}

        private void Setup()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.Log = null;
            //Database.Log = Console.Write;
            //Database.Log = log => EntityFrameworkHelper.Log(log, ZLibrary.ZDatabaseLogger.File);
            //Database.Log = log => EntityFrameworkHelper.Log(log, ZLibrary.ZDatabaseLogger.NLog);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ModuleInfo>().Map(t =>
            //{
            //    t.ToTable("ModuleInfo");
            //});

            modelBuilder.Configurations.Add(new ActivityConfiguration());
            modelBuilder.Configurations.Add(new ActivityRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }

        #endregion Methods
    }
}
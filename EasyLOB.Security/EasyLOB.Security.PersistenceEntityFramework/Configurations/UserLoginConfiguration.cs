using System.Data.Entity.ModelConfiguration;
using EasyLOB.Security.Data;

namespace EasyLOB.Security.Persistence
{
    public class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfiguration()
        {
            #region Class

            this.ToTable("AspNetUserLogins");

            this.HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            #endregion Class

            #region Properties

            this.Property(x => x.LoginProvider)
                .HasColumnName("LoginProvider")
                .HasColumnOrder(1)
                .HasColumnType("varchar")
                .HasMaxLength(128)
                .IsRequired();

            this.Property(x => x.ProviderKey)
                .HasColumnName("ProviderKey")
                .HasColumnOrder(2)
                .HasColumnType("varchar")
                .HasMaxLength(128)
                .IsRequired();

            this.Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnOrder(3)
                .HasColumnType("varchar")
                .HasMaxLength(128)
                .IsRequired();

            #endregion Properties

            #region Associations (FK)

            this.HasRequired(x => x.User)
                .WithMany(x => x.UserLogins)
                .HasForeignKey(x => x.UserId);

            #endregion Associations (FK)
        }
    }
}
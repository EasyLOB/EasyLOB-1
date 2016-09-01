using System.Data.Entity.ModelConfiguration;
using EasyLOB.Security.Data;

namespace EasyLOB.Security.Persistence
{
    public class ActivityRoleConfiguration : EntityTypeConfiguration<ActivityRole>
    {
        public ActivityRoleConfiguration()
        {
            #region Class

            this.ToTable("AspNetActivityRoles");

            this.HasKey(x => new { x.ActivityId, x.RoleId });

            #endregion Class

            #region Properties

            this.Property(x => x.ActivityId)
                .HasColumnName("ActivityId")
                .HasColumnOrder(1)
                .HasColumnType("varchar")
                .HasMaxLength(128)
                .IsRequired();

            this.Property(x => x.RoleId)
                .HasColumnName("RoleId")
                .HasColumnOrder(2)
                .HasColumnType("varchar")
                .HasMaxLength(128)
                .IsRequired();

            this.Property(x => x.Operations)
                .HasColumnName("Operations")
                .HasColumnType("varchar")
                .HasMaxLength(256);

            #endregion Properties

            #region Associations (FK)

            this.HasRequired(x => x.Activity)
                .WithMany(x => x.ActivityRoles)
                .HasForeignKey(x => x.ActivityId);

            this.HasRequired(x => x.Role)
                .WithMany(x => x.ActivityRoles)
                .HasForeignKey(x => x.RoleId);

            #endregion Associations (FK)
        }
    }
}
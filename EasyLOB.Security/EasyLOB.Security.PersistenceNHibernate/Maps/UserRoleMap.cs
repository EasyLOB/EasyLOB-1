using FluentNHibernate.Mapping;
using EasyLOB.Security.Data;

namespace EasyLOB.Security.Persistence
{
    public class UserRoleMap : ClassMap<UserRole>
    {
        public UserRoleMap()
        {
            #region Class

            Table("AspNetUserRoles");

            CompositeId()
                .KeyProperty(x => x.UserId, "UserId")
                .KeyProperty(x => x.RoleId, "RoleId");

            #endregion Class

            #region Associations (FK)

            References(x => x.Role)
                .Column("RoleId");

            References(x => x.User)
                .Column("UserId");

            #endregion Associations (FK)
        }
    }
}
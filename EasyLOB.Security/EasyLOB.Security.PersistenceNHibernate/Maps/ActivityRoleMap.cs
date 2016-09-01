using FluentNHibernate.Mapping;
using EasyLOB.Security.Data;

namespace EasyLOB.Security.Persistence
{
    public class ActivityRoleMap : ClassMap<ActivityRole>
    {
        public ActivityRoleMap()
        {
            #region Class

            Table("AspNetActivityRoles");

            CompositeId()
                .KeyProperty(x => x.ActivityId, "ActivityId")
                .KeyProperty(x => x.RoleId, "RoleId");

            #endregion Class

            #region Properties

            Map(x => x.Operations)
                .Column("Operations")
                .CustomSqlType("varchar")
                .Length(256);

            #endregion Properties

            #region Associations (FK)

            References(x => x.Activity)
                .Column("ActivityId");

            References(x => x.Role)
                .Column("RoleId");

            #endregion Associations (FK)
        }
    }
}
using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "Activity", "Role" },
        collections: new string[] { },
        isIdentity: false,
        keys: new string[] { "ActivityId", "RoleId" },
        linqOrderBy: "Operations",
        linqWhere: "ActivityId == @0 && RoleId == @1",
        lookup: "Operations"
    )]
    public partial class ActivityRole : ZDataBase
    {
        #region Properties

        private string _activityId;

        public virtual string ActivityId
        {
            get { return this.Activity == null ? _activityId : this.Activity.Id; }
            set
            {
                _activityId = value;
                Activity = null;
            }
        }

        private string _roleId;

        public virtual string RoleId
        {
            get { return this.Role == null ? _roleId : this.Role.Id; }
            set
            {
                _roleId = value;
                Role = null;
            }
        }

        public virtual string Operations { get; set; }

        #endregion Properties

        #region Properties ZDataBase

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }
         
        #endregion Properties ZDataBase

        #region Associations (FK)

        public virtual Activity Activity { get; set; } // ActivityId

        public virtual Role Role { get; set; } // RoleId

        #endregion Associations (FK)

        #region Methods

        public ActivityRole()
        {
            ActivityId = LibraryDefaults.Default_String;
            RoleId = LibraryDefaults.Default_String;
            Operations = null;

            //Activity = new Activity();
            //Role = new Role();
        }

        public ActivityRole(string activityId, string roleId)
            : this()
        {
            ActivityId = activityId;
            RoleId = roleId;
        }

        public ActivityRole(
            string activityId,
            string roleId,
            string operations = null
        )
            : this()
        {
            ActivityId = activityId;
            RoleId = roleId;
            Operations = operations;
        }

        public override object[] GetId()
        {
            return new object[] { ActivityId, RoleId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                ActivityId = DataHelper.IdToString(ids[0]);
            }
            if (ids != null && ids[1] != null)
            {
                RoleId = DataHelper.IdToString(ids[1]);
            }
        }

        #endregion Methods

        #region Methods NHibernate

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is ActivityRole)
            {
                var activityRole = (ActivityRole)obj;
                if (activityRole == null)
                {
                    return false;
                }

                if (ActivityId == activityRole.ActivityId && RoleId == activityRole.RoleId)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (ActivityId.ToString() + "|" + RoleId.ToString()).GetHashCode();
        }

        #endregion Methods NHibernate
    }
}
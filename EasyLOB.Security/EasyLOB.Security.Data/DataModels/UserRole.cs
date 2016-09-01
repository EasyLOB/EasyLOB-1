using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "Role", "User" },
        collections: new string[] { },
        isIdentity: false,
        keys: new string[] { "UserId", "RoleId" },
        linqOrderBy: "RoleId",
        linqWhere: "UserId == @0 && RoleId == @1",
        lookup: "RoleId"
    )]
    public partial class UserRole : ZDataBase
    {
        #region Properties

        private string _userId;

        public virtual string UserId
        {
            get { return this.User == null ? _userId : this.User.Id; }
            set
            {
                _userId = value;
                User = null;
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

        #endregion Properties

        #region Properties ZDataBase

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties ZDataBase

        #region Associations (FK)

        public virtual Role Role { get; set; } // RoleId

        public virtual User User { get; set; } // UserId

        #endregion Associations (FK)

        #region Methods

        public UserRole()
        {
            UserId = LibraryDefaults.Default_String;
            RoleId = LibraryDefaults.Default_String;

            //Role = new Role();
            //User = new User();
        }

        public UserRole(
            string userId,
            string roleId
        )
            : this()
        {
            UserId = userId;
            RoleId = roleId;
        }

        public override object[] GetId()
        {
            return new object[] { UserId, RoleId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                UserId = DataHelper.IdToString(ids[0]);
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

            if (obj is UserRole)
            {
                var userRole = (UserRole)obj;
                if (userRole == null)
                {
                    return false;
                }

                if (UserId == userRole.UserId && RoleId == userRole.RoleId)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (UserId.ToString() + "|" + RoleId.ToString()).GetHashCode();
        }

        #endregion Methods NHibernate
    }
}
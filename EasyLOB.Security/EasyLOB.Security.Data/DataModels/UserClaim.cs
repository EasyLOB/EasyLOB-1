using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "User" },
        collections: new string[] { },
        isIdentity: true,
        keys: new string[] { "Id" },
        linqOrderBy: "UserId",
        linqWhere: "Id == @0",
        lookup: "UserId"
    )]
    public partial class UserClaim : ZDataBase
    {
        #region Properties

        public virtual int Id { get; set; }

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

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        #endregion Properties

        #region Properties ZDataBase

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties ZData

        #region Associations (FK)

        public virtual User User { get; set; } // UserId

        #endregion Associations (FK)

        #region Methods

        public UserClaim()
        {
            Id = LibraryDefaults.Default_Int32;
            UserId = LibraryDefaults.Default_String;
            ClaimType = null;
            ClaimValue = null;

            //User = new User();
        }

        public UserClaim(int id)
            : this()
        {
            Id = id;
        }

        public UserClaim(
            int id,
            string userId,
            string claimType = null,
            string claimValue = null
        )
            : this()
        {
            Id = id;
            UserId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public override object[] GetId()
        {
            return new object[] { Id };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                Id = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}
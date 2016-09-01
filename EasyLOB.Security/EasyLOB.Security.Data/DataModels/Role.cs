using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { },
        collections: new string[] { "ActivityRoles", "UserRoles" },
        isIdentity: false,
        keys: new string[] { "Id" },
        linqOrderBy: "Name",
        linqWhere: "Id == @0",
        lookup: "Name"
    )]
    public partial class Role : ZDataBase
    {
        #region Properties

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Discriminator { get; set; }

        #endregion Properties

        #region Properties ZDataBase

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Collections (PK)

        public virtual IList<ActivityRole> ActivityRoles { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }

        #endregion Collections (PK)

        #region Methods

        public Role()
        {
            Id = LibraryDefaults.Default_String;
            Name = LibraryDefaults.Default_String;
            Discriminator = LibraryDefaults.Default_String;

            ActivityRoles = new List<ActivityRole>();
            UserRoles = new List<UserRole>();
        }

        public Role(string id)
            : this()
        {
            Id = id;
        }

        public Role(
            string id,
            string name,
            string discriminator
        )
            : this()
        {
            Id = id;
            Name = name;
            Discriminator = discriminator;
        }

        public override object[] GetId()
        {
            return new object[] { Id };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                Id = DataHelper.IdToString(ids[0]);
            }
        }

        #endregion Methods
    }
}
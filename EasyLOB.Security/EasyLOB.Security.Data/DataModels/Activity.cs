using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { },
        collections: new string[] { "ActivityRoles" },
        isIdentity: false,
        keys: new string[] { "Id" },
        linqOrderBy: "Name",
        linqWhere: "Id == @0",
        lookup: "Name"
    )]
    public partial class Activity : ZDataBase
    {
        #region Properties

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        #endregion Properties

        #region Collections (PK)

        public virtual IList<ActivityRole> ActivityRoles { get; }

        #endregion Collections (PK)

        #region Methods

        public Activity()
        {
            Id = LibraryDefaults.Default_String;
            Name = LibraryDefaults.Default_String;

            ActivityRoles = new List<ActivityRole>();
        }

        public Activity(string id)
            : this()
        {
            Id = id;
        }

        public Activity(
            string id,
            string name
        )
            : this()
        {
            Id = id;
            Name = name;
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
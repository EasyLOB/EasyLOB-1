using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class ActivityRoleDTO : ZDTOBase<ActivityRoleDTO, ActivityRole>
    {
        #region Properties
               
        public virtual string ActivityId { get; set; }
               
        public virtual string RoleId { get; set; }
               
        public virtual string Operations { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string ActivityLookupText { get; set; } // ActivityId

        public virtual string RoleLookupText { get; set; } // RoleId
    
        #endregion Associations FK

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public ActivityRoleDTO()
        {
        }
        
        public ActivityRoleDTO(
            string activityId,
            string roleId,
            string operations = null,
            string activityLookupText = null,
            string roleLookupText = null
        )
        {
            ActivityId = activityId;
            RoleId = roleId;
            Operations = operations;
            ActivityLookupText = activityLookupText;
            RoleLookupText = roleLookupText;
        }

        public ActivityRoleDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<ActivityRoleDTO, ActivityRole> GetDataSelector()
        {
            return x => new ActivityRole
            {
                ActivityId = x.ActivityId,
                RoleId = x.RoleId,
                Operations = x.Operations
            };
        }

        public override Func<ActivityRole, ActivityRoleDTO> GetDTOSelector()
        {
            return x => new ActivityRoleDTO
            {
                ActivityId = x.ActivityId,
                RoleId = x.RoleId,
                Operations = x.Operations,
                ActivityLookupText = x.Activity == null ? "" : x.Activity.LookupText,
                RoleLookupText = x.Role == null ? "" : x.Role.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                ActivityRoleDTO dto = (new List<ActivityRole> { (ActivityRole)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<ActivityRoleDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

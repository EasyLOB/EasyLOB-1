using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class UserRoleDTO : ZDTOBase<UserRoleDTO, UserRole>
    {
        #region Properties
               
        public virtual string UserId { get; set; }
               
        public virtual string RoleId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string RoleLookupText { get; set; } // RoleId

        public virtual string UserLookupText { get; set; } // UserId
    
        #endregion Associations FK

        #region Methods
        
        public UserRoleDTO()
        {
            UserId = LibraryDefaults.Default_String;
            RoleId = LibraryDefaults.Default_String;
        }
        
        public UserRoleDTO(
            string userId,
            string roleId,
            string roleLookupText = null,
            string userLookupText = null
        )
        {
            UserId = userId;
            RoleId = roleId;
            RoleLookupText = roleLookupText;
            UserLookupText = userLookupText;
        }

        public UserRoleDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<UserRoleDTO, UserRole> GetDataSelector()
        {
            return x => new UserRole
            {
                UserId = x.UserId,
                RoleId = x.RoleId
            };
        }

        public override Func<UserRole, UserRoleDTO> GetDTOSelector()
        {
            return x => new UserRoleDTO
            {
                UserId = x.UserId,
                RoleId = x.RoleId,
                RoleLookupText = x.Role == null ? "" : x.Role.LookupText,
                UserLookupText = x.User == null ? "" : x.User.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserRoleDTO dto = (new List<UserRole> { (UserRole)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<UserRoleDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

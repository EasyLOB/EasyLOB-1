using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Security.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class UserRoleViewModel : ZViewBase<UserRoleViewModel, UserRoleDTO, UserRole>
    {
        #region Properties
        
        [Display(Name = "PropertyUserId", ResourceType = typeof(UserRoleResources))]
        //[Key]
        //[Column(Order=1)]
        [Required]
        [StringLength(128)]
        public virtual string UserId { get; set; }
        
        [Display(Name = "PropertyRoleId", ResourceType = typeof(UserRoleResources))]
        //[Key]
        //[Column(Order=2)]
        [Required]
        [StringLength(128)]
        public virtual string RoleId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string RoleLookupText { get; set; } // RoleId

        public virtual string UserLookupText { get; set; } // UserId
    
        #endregion Associations FK

        #region Methods
        
        public UserRoleViewModel()
        {
            UserId = LibraryDefaults.Default_String;
            RoleId = LibraryDefaults.Default_String;
        }
        
        public UserRoleViewModel(
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

        public UserRoleViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public UserRoleViewModel(IZDTOBase<UserRoleDTO, UserRole> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<UserRoleViewModel, UserRoleDTO> GetDTOSelector()
        {
            return x => new UserRoleDTO
            {
                UserId = x.UserId,
                RoleId = x.RoleId
            };
        }

        public override Func<UserRoleDTO, UserRoleViewModel> GetViewSelector()
        {
            return x => new UserRoleViewModel
            {
                UserId = x.UserId,
                RoleId = x.RoleId,
                RoleLookupText = x.RoleLookupText,
                UserLookupText = x.UserLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserRoleDTO dto = new UserRoleDTO(data);
                UserRoleViewModel view = (new List<UserRoleDTO> { (UserRoleDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<UserRoleDTO, UserRole> dto)
        {
            if (dto != null)
            {
                UserRoleViewModel view = (new List<UserRoleDTO> { (UserRoleDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<UserRoleDTO, UserRole> ToDTO()
        {
            return (new List<UserRoleViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}

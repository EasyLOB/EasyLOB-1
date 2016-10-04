using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Security.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class UserClaimViewModel : ZViewBase<UserClaimViewModel, UserClaimDTO, UserClaim>
    {
        #region Properties
        
        [Display(Name = "PropertyId", ResourceType = typeof(UserClaimResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int Id { get; set; }
        
        [Display(Name = "PropertyUserId", ResourceType = typeof(UserClaimResources))]
        [Required]
        [StringLength(128)]
        public virtual string UserId { get; set; }
        
        [Display(Name = "PropertyClaimType", ResourceType = typeof(UserClaimResources))]
        [StringLength(1024)]
        public virtual string ClaimType { get; set; }
        
        [Display(Name = "PropertyClaimValue", ResourceType = typeof(UserClaimResources))]
        [StringLength(1024)]
        public virtual string ClaimValue { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string UserLookupText { get; set; } // UserId
    
        #endregion Associations FK

        #region Methods
        
        public UserClaimViewModel()
        {
            Id = LibraryDefaults.Default_Int32;
            UserId = LibraryDefaults.Default_String;
            ClaimType = null;
            ClaimValue = null;
        }
        
        public UserClaimViewModel(
            int id,
            string userId,
            string claimType = null,
            string claimValue = null,
            string userLookupText = null
        )
        {
            Id = id;
            UserId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
            UserLookupText = userLookupText;
        }

        public UserClaimViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public UserClaimViewModel(IZDTOBase<UserClaimDTO, UserClaim> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<UserClaimViewModel, UserClaimDTO> GetDTOSelector()
        {
            return x => new UserClaimDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                ClaimType = x.ClaimType,
                ClaimValue = x.ClaimValue
            };
        }

        public override Func<UserClaimDTO, UserClaimViewModel> GetViewSelector()
        {
            return x => new UserClaimViewModel
            {
                Id = x.Id,
                UserId = x.UserId,
                ClaimType = x.ClaimType,
                ClaimValue = x.ClaimValue,
                UserLookupText = x.UserLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserClaimDTO dto = new UserClaimDTO(data);
                UserClaimViewModel view = (new List<UserClaimDTO> { (UserClaimDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<UserClaimDTO, UserClaim> dto)
        {
            if (dto != null)
            {
                UserClaimViewModel view = (new List<UserClaimDTO> { (UserClaimDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<UserClaimDTO, UserClaim> ToDTO()
        {
            return (new List<UserClaimViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}

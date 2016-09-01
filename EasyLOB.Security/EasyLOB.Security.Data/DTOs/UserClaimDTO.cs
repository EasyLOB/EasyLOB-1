using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class UserClaimDTO : ZDTOBase<UserClaimDTO, UserClaim>
    {
        #region Properties
               
        public virtual int Id { get; set; }
               
        public virtual string UserId { get; set; }
               
        public virtual string ClaimType { get; set; }
               
        public virtual string ClaimValue { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string UserLookupText { get; set; } // UserId
    
        #endregion Associations FK

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public UserClaimDTO()
        {
        }
        
        public UserClaimDTO(
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

        public UserClaimDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<UserClaimDTO, UserClaim> GetDataSelector()
        {
            return x => new UserClaim
            {
                Id = x.Id,
                UserId = x.UserId,
                ClaimType = x.ClaimType,
                ClaimValue = x.ClaimValue
            };
        }

        public override Func<UserClaim, UserClaimDTO> GetDTOSelector()
        {
            return x => new UserClaimDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                ClaimType = x.ClaimType,
                ClaimValue = x.ClaimValue,
                UserLookupText = x.User == null ? "" : x.User.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserClaimDTO dto = (new List<UserClaim> { (UserClaim)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<UserClaimDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

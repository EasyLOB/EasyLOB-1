using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Security.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class UserLoginViewModel : ZViewBase<UserLoginViewModel, UserLoginDTO, UserLogin>
    {
        #region Properties
        
        [Display(Name = "PropertyLoginProvider", ResourceType = typeof(UserLoginResources))]
        //[Key]
        //[Column(Order=1)]
        [Required]
        [StringLength(128)]
        public virtual string LoginProvider { get; set; }
        
        [Display(Name = "PropertyProviderKey", ResourceType = typeof(UserLoginResources))]
        //[Key]
        //[Column(Order=2)]
        [Required]
        [StringLength(128)]
        public virtual string ProviderKey { get; set; }
        
        [Display(Name = "PropertyUserId", ResourceType = typeof(UserLoginResources))]
        //[Key]
        //[Column(Order=3)]
        [Required]
        [StringLength(128)]
        public virtual string UserId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string UserLookupText { get; set; } // UserId
    
        #endregion Associations FK

        #region Properties ZViewBase

        public override string LookupText { get; set; }

        #endregion Properties ZViewBase

        #region Methods
        
        public UserLoginViewModel()
        {
            LoginProvider = "A";
            ProviderKey = "A";
            UserId = "A";
        }
        
        public UserLoginViewModel(
            string loginProvider,
            string providerKey,
            string userId,
            string userLookupText = null
        )
            : this()
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            UserId = userId;
            UserLookupText = userLookupText;
        }

        public UserLoginViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public UserLoginViewModel(IZDTOBase<UserLoginDTO, UserLogin> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<UserLoginViewModel, UserLoginDTO> GetDTOSelector()
        {
            return x => new UserLoginDTO
            {
                LoginProvider = x.LoginProvider,
                ProviderKey = x.ProviderKey,
                UserId = x.UserId,
                UserLookupText = x.UserLookupText,
                LookupText = x.LookupText
            };
        }

        public override Func<UserLoginDTO, UserLoginViewModel> GetViewSelector()
        {
            return x => new UserLoginViewModel
            {
                LoginProvider = x.LoginProvider,
                ProviderKey = x.ProviderKey,
                UserId = x.UserId,
                UserLookupText = x.UserLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserLoginDTO dto = new UserLoginDTO(data);
                UserLoginViewModel view = (new List<UserLoginDTO> { (UserLoginDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<UserLoginDTO, UserLogin> dto)
        {
            if (dto != null)
            {
                UserLoginViewModel view = (new List<UserLoginDTO> { (UserLoginDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<UserLoginDTO, UserLogin> ToDTO()
        {
            return (new List<UserLoginViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}

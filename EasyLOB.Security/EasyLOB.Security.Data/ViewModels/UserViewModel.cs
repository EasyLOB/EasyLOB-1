using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Security.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

// !!!

namespace EasyLOB.Security.Data
{
    public partial class UserViewModel : ZViewBase<UserViewModel, UserDTO, User>
    {
        #region Properties

        [Display(Name = "PropertyId", ResourceType = typeof(UserResources))]
        //[Key]
        [Required]
        [StringLength(128)]
        public virtual string Id { get; set; }

        [Display(Name = "PropertyUserName", ResourceType = typeof(UserResources))]
        [Required]
        [StringLength(256)]
        public virtual string UserName { get; set; }

        [Display(Name = "PropertyEmail", ResourceType = typeof(UserResources))]
        [StringLength(256)]
        public virtual string Email { get; set; }

        [Display(Name = "PropertyPassword", ResourceType = typeof(UserResources))]
        [StringLength(1024)]
        public virtual string Password { get; set; }

        //[Display(Name = "PropertyEmailConfirmed", ResourceType = typeof(UserResources))]
        //[Required]
        //public virtual bool EmailConfirmed { get; set; }

        //[Display(Name = "PropertyPasswordHash", ResourceType = typeof(UserResources))]
        //[StringLength(1024)]
        //public virtual string PasswordHash { get; set; }

        //[Display(Name = "PropertySecurityStamp", ResourceType = typeof(UserResources))]
        //[StringLength(1024)]
        //public virtual string SecurityStamp { get; set; }

        //[Display(Name = "PropertyPhoneNumber", ResourceType = typeof(UserResources))]
        //[StringLength(1024)]
        //public virtual string PhoneNumber { get; set; }

        //[Display(Name = "PropertyPhoneNumberConfirmed", ResourceType = typeof(UserResources))]
        //[Required]
        //public virtual bool PhoneNumberConfirmed { get; set; }

        //[Display(Name = "PropertyTwoFactorEnabled", ResourceType = typeof(UserResources))]
        //[Required]
        //public virtual bool TwoFactorEnabled { get; set; }

        //[Display(Name = "PropertyLockoutEndDateUtc", ResourceType = typeof(UserResources))]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //public virtual DateTime? LockoutEndDateUtc { get; set; }

        //[Display(Name = "PropertyLockoutEnabled", ResourceType = typeof(UserResources))]
        //[Required]
        //public virtual bool LockoutEnabled { get; set; }

        //[Display(Name = "PropertyAccessFailedCount", ResourceType = typeof(UserResources))]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Range(1, Int32.MaxValue)]
        //[Required]
        //public virtual int AccessFailedCount { get; set; }

        #endregion Properties

        #region Properties ZViewBase

        public override string LookupText { get; set; }

        #endregion Properties ZViewBase

        #region Methods

        public UserViewModel()
        {
            // !!!
            Id = Guid.NewGuid().ToString();            
        }

        public UserViewModel(
            string id,
            string userName,
            string email = null
            //bool emailConfirmed,
            //bool phoneNumberConfirmed,
            //bool twoFactorEnabled,
            //bool lockoutEnabled,
            //int accessFailedCount,
            //string passwordHash = null,
            //string securityStamp = null,
            //string phoneNumber = null,
            //DateTime? lockoutEndDateUtc = null
        )
            : this()
        {
            Id = id;
            UserName = userName;
            Email = email;
            //EmailConfirmed = emailConfirmed;
            //PasswordHash = passwordHash;
            //SecurityStamp = securityStamp;
            //PhoneNumber = phoneNumber;
            //PhoneNumberConfirmed = phoneNumberConfirmed;
            //TwoFactorEnabled = twoFactorEnabled;
            //LockoutEndDateUtc = lockoutEndDateUtc;
            //LockoutEnabled = lockoutEnabled;
            //AccessFailedCount = accessFailedCount;
        }

        public UserViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public UserViewModel(IZDTOBase<UserDTO, User> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<UserViewModel, UserDTO> GetDTOSelector()
        {
            return x => new UserDTO
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                //EmailConfirmed = x.EmailConfirmed,
                //PasswordHash = x.PasswordHash,
                //SecurityStamp = x.SecurityStamp,
                //PhoneNumber = x.PhoneNumber,
                //PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                //TwoFactorEnabled = x.TwoFactorEnabled,
                //LockoutEndDateUtc = x.LockoutEndDateUtc,
                //LockoutEnabled = x.LockoutEnabled,
                //AccessFailedCount = x.AccessFailedCount,
                LookupText = x.LookupText
            };
        }

        public override Func<UserDTO, UserViewModel> GetViewSelector()
        {
            return x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                //EmailConfirmed = x.EmailConfirmed,
                //PasswordHash = x.PasswordHash,
                //SecurityStamp = x.SecurityStamp,
                //PhoneNumber = x.PhoneNumber,
                //PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                //TwoFactorEnabled = x.TwoFactorEnabled,
                //LockoutEndDateUtc = x.LockoutEndDateUtc,
                //LockoutEnabled = x.LockoutEnabled,
                //AccessFailedCount = x.AccessFailedCount,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserDTO dto = new UserDTO(data);
                UserViewModel view = (new List<UserDTO> { (UserDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<UserDTO, User> dto)
        {
            if (dto != null)
            {
                UserViewModel view = (new List<UserDTO> { (UserDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }

        public override IZDTOBase<UserDTO, User> ToDTO()
        {
            return (new List<UserViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZViewBase
    }
}
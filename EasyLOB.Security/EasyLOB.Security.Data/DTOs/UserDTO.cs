using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class UserDTO : ZDTOBase<UserDTO, User>
    {
        #region Properties
               
        public virtual string Id { get; set; }
               
        public virtual string Email { get; set; }
               
        public virtual bool EmailConfirmed { get; set; }
               
        public virtual string PasswordHash { get; set; }
               
        public virtual string SecurityStamp { get; set; }
               
        public virtual string PhoneNumber { get; set; }
               
        public virtual bool PhoneNumberConfirmed { get; set; }
               
        public virtual bool TwoFactorEnabled { get; set; }
               
        public virtual DateTime? LockoutEndDateUtc { get; set; }
               
        public virtual bool LockoutEnabled { get; set; }
               
        public virtual int AccessFailedCount { get; set; }
               
        public virtual string UserName { get; set; }

        #endregion Properties

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public UserDTO()
        {
        }
        
        public UserDTO(
            string id,
            bool emailConfirmed,
            bool phoneNumberConfirmed,
            bool twoFactorEnabled,
            bool lockoutEnabled,
            int accessFailedCount,
            string userName,
            string email = null,
            string passwordHash = null,
            string securityStamp = null,
            string phoneNumber = null,
            DateTime? lockoutEndDateUtc = null
        )
        {
            Id = id;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEndDateUtc = lockoutEndDateUtc;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            UserName = userName;
        }

        public UserDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<UserDTO, User> GetDataSelector()
        {
            return x => new User
            {
                Id = x.Id,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                PasswordHash = x.PasswordHash,
                SecurityStamp = x.SecurityStamp,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                TwoFactorEnabled = x.TwoFactorEnabled,
                LockoutEndDateUtc = x.LockoutEndDateUtc,
                LockoutEnabled = x.LockoutEnabled,
                AccessFailedCount = x.AccessFailedCount,
                UserName = x.UserName
            };
        }

        public override Func<User, UserDTO> GetDTOSelector()
        {
            return x => new UserDTO
            {
                Id = x.Id,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                PasswordHash = x.PasswordHash,
                SecurityStamp = x.SecurityStamp,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                TwoFactorEnabled = x.TwoFactorEnabled,
                LockoutEndDateUtc = x.LockoutEndDateUtc,
                LockoutEnabled = x.LockoutEnabled,
                AccessFailedCount = x.AccessFailedCount,
                UserName = x.UserName,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserDTO dto = (new List<User> { (User)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<UserDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

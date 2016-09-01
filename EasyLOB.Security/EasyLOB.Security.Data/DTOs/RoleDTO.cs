using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class RoleDTO : ZDTOBase<RoleDTO, Role>
    {
        #region Properties
               
        public virtual string Id { get; set; }
               
        public virtual string Name { get; set; }
               
        public virtual string Discriminator { get; set; }

        #endregion Properties

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public RoleDTO()
        {
        }
        
        public RoleDTO(
            string id,
            string name,
            string discriminator
        )
        {
            Id = id;
            Name = name;
            Discriminator = discriminator;
        }

        public RoleDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<RoleDTO, Role> GetDataSelector()
        {
            return x => new Role
            {
                Id = x.Id,
                Name = x.Name,
                Discriminator = x.Discriminator
            };
        }

        public override Func<Role, RoleDTO> GetDTOSelector()
        {
            return x => new RoleDTO
            {
                Id = x.Id,
                Name = x.Name,
                Discriminator = x.Discriminator,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                RoleDTO dto = (new List<Role> { (Role)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<RoleDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

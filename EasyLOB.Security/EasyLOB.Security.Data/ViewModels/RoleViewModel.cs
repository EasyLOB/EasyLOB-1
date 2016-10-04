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
    public partial class RoleViewModel : ZViewBase<RoleViewModel, RoleDTO, Role>
    {
        #region Properties

        [Display(Name = "PropertyId", ResourceType = typeof(RoleResources))]
        //[Key]
        [Required]
        [StringLength(128)]
        public virtual string Id { get; set; }

        [Display(Name = "PropertyName", ResourceType = typeof(RoleResources))]
        [Required]
        [StringLength(256)]
        public virtual string Name { get; set; }

        //[Display(Name = "PropertyDiscriminator", ResourceType = typeof(RoleResources))]
        //[Required]
        //[StringLength(128)]
        //public virtual string Discriminator { get; set; }

        #endregion Properties

        #region Methods

        public RoleViewModel()
        {
            //Id = LibraryDefaults.Default_String;
            Id = Guid.NewGuid().ToString(); // !!! 
            Name = LibraryDefaults.Default_String;
            //Discriminator = LibraryDefaults.Default_String;
        }

        public RoleViewModel(
            string id,
            string name
            //string discriminator
        )
        {
            Id = id;
            Name = name;
            //Discriminator = discriminator;
        }

        public RoleViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public RoleViewModel(IZDTOBase<RoleDTO, Role> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<RoleViewModel, RoleDTO> GetDTOSelector()
        {
            return x => new RoleDTO
            {
                Id = x.Id,
                Name = x.Name,
                //Discriminator = x.Discriminator
            };
        }

        public override Func<RoleDTO, RoleViewModel> GetViewSelector()
        {
            return x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                //Discriminator = x.Discriminator,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                RoleDTO dto = new RoleDTO(data);
                RoleViewModel view = (new List<RoleDTO> { (RoleDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<RoleDTO, Role> dto)
        {
            if (dto != null)
            {
                RoleViewModel view = (new List<RoleDTO> { (RoleDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }

        public override IZDTOBase<RoleDTO, Role> ToDTO()
        {
            return (new List<RoleViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZViewBase
    }
}
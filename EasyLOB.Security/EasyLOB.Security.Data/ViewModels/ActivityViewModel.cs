using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Security.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class ActivityViewModel : ZViewBase<ActivityViewModel, ActivityDTO, EasyLOB.Security.Data.Activity> // !!! Namespace
    {
        #region Properties

        [Display(Name = "PropertyId", ResourceType = typeof(ActivityResources))]
        //[Key]
        [Required]
        [StringLength(128)]
        public virtual string Id { get; set; }

        [Display(Name = "PropertyName", ResourceType = typeof(ActivityResources))]
        [Required]
        [StringLength(256)]
        public virtual string Name { get; set; }

        #endregion Properties

        #region Properties ZViewBase

        public override string LookupText { get; set; }

        #endregion Properties ZViewBase

        #region Methods

        public ActivityViewModel()
        {
            // !!!
            Id = Guid.NewGuid().ToString();
        }

        public ActivityViewModel(
            string id,
            string name
        )
            : this()
        {
            Id = id;
            Name = name;
        }

        public ActivityViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public ActivityViewModel(IZDTOBase<ActivityDTO, Activity> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<ActivityViewModel, ActivityDTO> GetDTOSelector()
        {
            return x => new ActivityDTO
            {
                Id = x.Id,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override Func<ActivityDTO, ActivityViewModel> GetViewSelector()
        {
            return x => new ActivityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                ActivityDTO dto = new ActivityDTO(data);
                ActivityViewModel view = (new List<ActivityDTO> { (ActivityDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<ActivityDTO, EasyLOB.Security.Data.Activity> dto) // !!! Namespace
        {
            if (dto != null)
            {
                ActivityViewModel view = (new List<ActivityDTO> { (ActivityDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }

        public override IZDTOBase<ActivityDTO, EasyLOB.Security.Data.Activity> ToDTO() // !!! Namespace
        {
            return (new List<ActivityViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZViewBase
    }
}
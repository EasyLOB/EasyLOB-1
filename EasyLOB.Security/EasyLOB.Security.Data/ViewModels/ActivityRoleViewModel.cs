using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Security.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class ActivityRoleViewModel : ZViewBase<ActivityRoleViewModel, ActivityRoleDTO, ActivityRole>
    {
        #region Properties
        
        [Display(Name = "PropertyActivityId", ResourceType = typeof(ActivityRoleResources))]
        //[Key]
        //[Column(Order=1)]
        [Required]
        [StringLength(128)]
        public virtual string ActivityId { get; set; }
        
        [Display(Name = "PropertyRoleId", ResourceType = typeof(ActivityRoleResources))]
        //[Key]
        //[Column(Order=2)]
        [Required]
        [StringLength(128)]
        public virtual string RoleId { get; set; }
        
        [Display(Name = "PropertyOperations", ResourceType = typeof(ActivityRoleResources))]
        [StringLength(256)]
        public virtual string Operations { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string ActivityLookupText { get; set; } // ActivityId

        public virtual string RoleLookupText { get; set; } // RoleId
    
        #endregion Associations FK

        #region Properties ZViewBase

        public override string LookupText { get; set; }

        #endregion Properties ZViewBase

        #region Methods
        
        public ActivityRoleViewModel()
        {
            ActivityId = "A";
            RoleId = "A";
        }
        
        public ActivityRoleViewModel(
            string activityId,
            string roleId,
            string operations = null,
            string activityLookupText = null,
            string roleLookupText = null
        )
            : this()
        {
            ActivityId = activityId;
            RoleId = roleId;
            Operations = operations;
            ActivityLookupText = activityLookupText;
            RoleLookupText = roleLookupText;
        }

        public ActivityRoleViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public ActivityRoleViewModel(IZDTOBase<ActivityRoleDTO, ActivityRole> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<ActivityRoleViewModel, ActivityRoleDTO> GetDTOSelector()
        {
            return x => new ActivityRoleDTO
            {
                ActivityId = x.ActivityId,
                RoleId = x.RoleId,
                Operations = x.Operations,
                ActivityLookupText = x.ActivityLookupText,
                RoleLookupText = x.RoleLookupText,
                LookupText = x.LookupText
            };
        }

        public override Func<ActivityRoleDTO, ActivityRoleViewModel> GetViewSelector()
        {
            return x => new ActivityRoleViewModel
            {
                ActivityId = x.ActivityId,
                RoleId = x.RoleId,
                Operations = x.Operations,
                ActivityLookupText = x.ActivityLookupText,
                RoleLookupText = x.RoleLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                ActivityRoleDTO dto = new ActivityRoleDTO(data);
                ActivityRoleViewModel view = (new List<ActivityRoleDTO> { (ActivityRoleDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<ActivityRoleDTO, ActivityRole> dto)
        {
            if (dto != null)
            {
                ActivityRoleViewModel view = (new List<ActivityRoleDTO> { (ActivityRoleDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<ActivityRoleDTO, ActivityRole> ToDTO()
        {
            return (new List<ActivityRoleViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}

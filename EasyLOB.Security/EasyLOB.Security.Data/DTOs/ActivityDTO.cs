using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Security.Data
{
    public partial class ActivityDTO : ZDTOBase<ActivityDTO, Activity>
    {
        #region Properties
               
        public virtual string Id { get; set; }
               
        public virtual string Name { get; set; }

        #endregion Properties

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public ActivityDTO()
        {
        }
        
        public ActivityDTO(
            string id,
            string name
        )
        {
            Id = id;
            Name = name;
        }

        public ActivityDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<ActivityDTO, Activity> GetDataSelector()
        {
            return x => new Activity
            {
                Id = x.Id,
                Name = x.Name
            };
        }

        public override Func<Activity, ActivityDTO> GetDTOSelector()
        {
            return x => new ActivityDTO
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
                ActivityDTO dto = (new List<Activity> { (Activity)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<ActivityDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

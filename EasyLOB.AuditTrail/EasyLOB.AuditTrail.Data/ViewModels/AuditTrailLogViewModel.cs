using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.AuditTrail.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail.Data
{
    public partial class AuditTrailLogViewModel : ZViewBase<AuditTrailLogViewModel, AuditTrailLogDTO, AuditTrailLog>
    {
        #region Properties
        
        [Display(Name = "PropertyAuditTrailLogId", ResourceType = typeof(AuditTrailLogResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int AuditTrailLogId { get; set; }
        
        [Display(Name = "PropertyLogDate", ResourceType = typeof(AuditTrailLogResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LogDate { get; set; }
        
        [Display(Name = "PropertyLogTime", ResourceType = typeof(AuditTrailLogResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LogTime { get; set; }
        
        [Display(Name = "PropertyLogUserName", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(256)]
        public virtual string LogUserName { get; set; }
        
        [Display(Name = "PropertyLogDomain", ResourceType = typeof(AuditTrailLogResources))]
        [Required]
        [StringLength(256)]
        public virtual string LogDomain { get; set; }
        
        [Display(Name = "PropertyLogEntity", ResourceType = typeof(AuditTrailLogResources))]
        [Required]
        [StringLength(256)]
        public virtual string LogEntity { get; set; }
        
        [Display(Name = "PropertyLogOperation", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(1)]
        public virtual string LogOperation { get; set; }
        
        [Display(Name = "PropertyLogId", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(4096)]
        public virtual string LogId { get; set; }
        
        [Display(Name = "PropertyLogEntityBefore", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(4096)]
        public virtual string LogEntityBefore { get; set; }
        
        [Display(Name = "PropertyLogEntityAfter", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(4096)]
        public virtual string LogEntityAfter { get; set; }

        #endregion Properties

        #region Methods
        
        public AuditTrailLogViewModel()
        {
            AuditTrailLogId = LibraryDefaults.Default_Int32;
            LogDomain = LibraryDefaults.Default_String;
            LogEntity = LibraryDefaults.Default_String;
            LogDate = null;
            LogTime = null;
            LogUserName = null;
            LogOperation = null;
            LogId = null;
            LogEntityBefore = null;
            LogEntityAfter = null;
        }
        
        public AuditTrailLogViewModel(
            int auditTrailLogId,
            string logDomain,
            string logEntity,
            DateTime? logDate = null,
            DateTime? logTime = null,
            string logUserName = null,
            string logOperation = null,
            string logId = null,
            string logEntityBefore = null,
            string logEntityAfter = null
        )
        {
            AuditTrailLogId = auditTrailLogId;
            LogDate = logDate;
            LogTime = logTime;
            LogUserName = logUserName;
            LogDomain = logDomain;
            LogEntity = logEntity;
            LogOperation = logOperation;
            LogId = logId;
            LogEntityBefore = logEntityBefore;
            LogEntityAfter = logEntityAfter;
        }

        public AuditTrailLogViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public AuditTrailLogViewModel(IZDTOBase<AuditTrailLogDTO, AuditTrailLog> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<AuditTrailLogViewModel, AuditTrailLogDTO> GetDTOSelector()
        {
            return x => new AuditTrailLogDTO
            {
                AuditTrailLogId = x.AuditTrailLogId,
                LogDate = x.LogDate,
                LogTime = x.LogTime,
                LogUserName = x.LogUserName,
                LogDomain = x.LogDomain,
                LogEntity = x.LogEntity,
                LogOperation = x.LogOperation,
                LogId = x.LogId,
                LogEntityBefore = x.LogEntityBefore,
                LogEntityAfter = x.LogEntityAfter
            };
        }

        public override Func<AuditTrailLogDTO, AuditTrailLogViewModel> GetViewSelector()
        {
            return x => new AuditTrailLogViewModel
            {
                AuditTrailLogId = x.AuditTrailLogId,
                LogDate = x.LogDate,
                LogTime = x.LogTime,
                LogUserName = x.LogUserName,
                LogDomain = x.LogDomain,
                LogEntity = x.LogEntity,
                LogOperation = x.LogOperation,
                LogId = x.LogId,
                LogEntityBefore = x.LogEntityBefore,
                LogEntityAfter = x.LogEntityAfter,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                AuditTrailLogDTO dto = new AuditTrailLogDTO(data);
                AuditTrailLogViewModel view = (new List<AuditTrailLogDTO> { (AuditTrailLogDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<AuditTrailLogDTO, AuditTrailLog> dto)
        {
            if (dto != null)
            {
                AuditTrailLogViewModel view = (new List<AuditTrailLogDTO> { (AuditTrailLogDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<AuditTrailLogDTO, AuditTrailLog> ToDTO()
        {
            return (new List<AuditTrailLogViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}

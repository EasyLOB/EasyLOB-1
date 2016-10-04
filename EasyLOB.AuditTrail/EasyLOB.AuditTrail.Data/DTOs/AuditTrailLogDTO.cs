using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail.Data
{
    public partial class AuditTrailLogDTO : ZDTOBase<AuditTrailLogDTO, AuditTrailLog>
    {
        #region Properties
               
        public virtual int AuditTrailLogId { get; set; }
               
        public virtual DateTime? LogDate { get; set; }
               
        public virtual DateTime? LogTime { get; set; }
               
        public virtual string LogUserName { get; set; }
               
        public virtual string LogDomain { get; set; }
               
        public virtual string LogEntity { get; set; }
               
        public virtual string LogOperation { get; set; }
               
        public virtual string LogId { get; set; }
               
        public virtual string LogEntityBefore { get; set; }
               
        public virtual string LogEntityAfter { get; set; }

        #endregion Properties

        #region Methods
        
        public AuditTrailLogDTO()
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
        
        public AuditTrailLogDTO(
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

        public AuditTrailLogDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<AuditTrailLogDTO, AuditTrailLog> GetDataSelector()
        {
            return x => new AuditTrailLog
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

        public override Func<AuditTrailLog, AuditTrailLogDTO> GetDTOSelector()
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
                LogEntityAfter = x.LogEntityAfter,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                AuditTrailLogDTO dto = (new List<AuditTrailLog> { (AuditTrailLog)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<AuditTrailLogDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { },
        collections: new string[] { },
        isIdentity: true,
        keys: new string[] { "AuditTrailLogId" },
        linqOrderBy: "LogUserName",
        linqWhere: "AuditTrailLogId == @0",
        lookup: "LogUserName"
    )]    
    public partial class AuditTrailLog : ZDataBase
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
        
        public AuditTrailLog()
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

        public AuditTrailLog(int auditTrailLogId)
            : this()
        {            
            AuditTrailLogId = auditTrailLogId;
        }

        public AuditTrailLog(
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
            : this()
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

        public override object[] GetId()
        {
            return new object[] { AuditTrailLogId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                AuditTrailLogId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}

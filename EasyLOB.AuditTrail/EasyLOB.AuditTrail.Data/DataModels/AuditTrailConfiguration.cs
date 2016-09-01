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
        keys: new string[] { "AuditTrailConfigurationId" },
        linqOrderBy: "Domain",
        linqWhere: "AuditTrailConfigurationId == @0",
        lookup: "Domain"
    )]    
    public partial class AuditTrailConfiguration : ZDataBase
    {        
        #region Properties
        
        public virtual int AuditTrailConfigurationId { get; set; }
        
        public virtual string Domain { get; set; }
        
        public virtual string Entity { get; set; }
        
        public virtual string LogOperations { get; set; }
        
        public virtual string LogMode { get; set; }

        #endregion Properties

        #region Properties ZDataBase

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties ZDataBase

        #region Methods
        
        public AuditTrailConfiguration()
        {            
            AuditTrailConfigurationId = LibraryDefaults.Default_Int32;
            Domain = LibraryDefaults.Default_String;
            Entity = LibraryDefaults.Default_String;
            LogOperations = null;
            LogMode = null;
        }

        public AuditTrailConfiguration(int auditTrailConfigurationId)
            : this()
        {            
            AuditTrailConfigurationId = auditTrailConfigurationId;
        }

        public AuditTrailConfiguration(
            int auditTrailConfigurationId,
            string domain,
            string entity,
            string logOperations = null,
            string logMode = null
        )
            : this()
        {
            AuditTrailConfigurationId = auditTrailConfigurationId;
            Domain = domain;
            Entity = entity;
            LogOperations = logOperations;
            LogMode = logMode;
        }

        public override object[] GetId()
        {
            return new object[] { AuditTrailConfigurationId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                AuditTrailConfigurationId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}

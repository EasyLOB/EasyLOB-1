using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail.Data
{
    public partial class AuditTrailConfigurationDTO : ZDTOBase<AuditTrailConfigurationDTO, AuditTrailConfiguration>
    {
        #region Properties
               
        public virtual int AuditTrailConfigurationId { get; set; }
               
        public virtual string Domain { get; set; }
               
        public virtual string Entity { get; set; }
               
        public virtual string LogOperations { get; set; }
               
        public virtual string LogMode { get; set; }

        #endregion Properties

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public AuditTrailConfigurationDTO()
        {
        }
        
        public AuditTrailConfigurationDTO(
            int auditTrailConfigurationId,
            string domain,
            string entity,
            string logOperations = null,
            string logMode = null
        )
        {
            AuditTrailConfigurationId = auditTrailConfigurationId;
            Domain = domain;
            Entity = entity;
            LogOperations = logOperations;
            LogMode = logMode;
        }

        public AuditTrailConfigurationDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<AuditTrailConfigurationDTO, AuditTrailConfiguration> GetDataSelector()
        {
            return x => new AuditTrailConfiguration
            {
                AuditTrailConfigurationId = x.AuditTrailConfigurationId,
                Domain = x.Domain,
                Entity = x.Entity,
                LogOperations = x.LogOperations,
                LogMode = x.LogMode
            };
        }

        public override Func<AuditTrailConfiguration, AuditTrailConfigurationDTO> GetDTOSelector()
        {
            return x => new AuditTrailConfigurationDTO
            {
                AuditTrailConfigurationId = x.AuditTrailConfigurationId,
                Domain = x.Domain,
                Entity = x.Entity,
                LogOperations = x.LogOperations,
                LogMode = x.LogMode,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                AuditTrailConfigurationDTO dto = (new List<AuditTrailConfiguration> { (AuditTrailConfiguration)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<AuditTrailConfigurationDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}

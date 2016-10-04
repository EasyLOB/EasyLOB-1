using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.AuditTrail.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail.Data
{
    public partial class AuditTrailConfigurationViewModel : ZViewBase<AuditTrailConfigurationViewModel, AuditTrailConfigurationDTO, AuditTrailConfiguration>
    {
        #region Properties
        
        [Display(Name = "PropertyAuditTrailConfigurationId", ResourceType = typeof(AuditTrailConfigurationResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int AuditTrailConfigurationId { get; set; }
        
        [Display(Name = "PropertyDomain", ResourceType = typeof(AuditTrailConfigurationResources))]
        [Required]
        [StringLength(256)]
        public virtual string Domain { get; set; }
        
        [Display(Name = "PropertyEntity", ResourceType = typeof(AuditTrailConfigurationResources))]
        [Required]
        [StringLength(256)]
        public virtual string Entity { get; set; }
        
        [Display(Name = "PropertyLogOperations", ResourceType = typeof(AuditTrailConfigurationResources))]
        [StringLength(256)]
        public virtual string LogOperations { get; set; }
        
        [Display(Name = "PropertyLogMode", ResourceType = typeof(AuditTrailConfigurationResources))]
        [StringLength(1)]
        public virtual string LogMode { get; set; }

        #endregion Properties

        #region Methods
        
        public AuditTrailConfigurationViewModel()
        {
            AuditTrailConfigurationId = LibraryDefaults.Default_Int32;
            Domain = LibraryDefaults.Default_String;
            Entity = LibraryDefaults.Default_String;
            LogOperations = null;
            LogMode = null;
        }
        
        public AuditTrailConfigurationViewModel(
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

        public AuditTrailConfigurationViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public AuditTrailConfigurationViewModel(IZDTOBase<AuditTrailConfigurationDTO, AuditTrailConfiguration> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<AuditTrailConfigurationViewModel, AuditTrailConfigurationDTO> GetDTOSelector()
        {
            return x => new AuditTrailConfigurationDTO
            {
                AuditTrailConfigurationId = x.AuditTrailConfigurationId,
                Domain = x.Domain,
                Entity = x.Entity,
                LogOperations = x.LogOperations,
                LogMode = x.LogMode
            };
        }

        public override Func<AuditTrailConfigurationDTO, AuditTrailConfigurationViewModel> GetViewSelector()
        {
            return x => new AuditTrailConfigurationViewModel
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
                AuditTrailConfigurationDTO dto = new AuditTrailConfigurationDTO(data);
                AuditTrailConfigurationViewModel view = (new List<AuditTrailConfigurationDTO> { (AuditTrailConfigurationDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<AuditTrailConfigurationDTO, AuditTrailConfiguration> dto)
        {
            if (dto != null)
            {
                AuditTrailConfigurationViewModel view = (new List<AuditTrailConfigurationDTO> { (AuditTrailConfigurationDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<AuditTrailConfigurationDTO, AuditTrailConfiguration> ToDTO()
        {
            return (new List<AuditTrailConfigurationViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}

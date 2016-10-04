using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLOB.Data
{
    public class ZViewBase<TEntityView, TEntityDTO, TEntity> : IZViewBase<TEntityView, TEntityDTO, TEntity>, IValidatableObject
    {
        #region Properties

        public virtual string LookupText { get; set; }

        #endregion Properties

        #region Methods

        public virtual Func<TEntityView, TEntityDTO> GetDTOSelector()
        {
            throw new NotImplementedException();
        }

        public virtual Func<TEntityDTO, TEntityView> GetViewSelector()
        {
            throw new NotImplementedException();
        }

        public virtual void FromData(IZDataBase data)
        {
            throw new NotImplementedException();
        }

        public virtual void FromDTO(IZDTOBase<TEntityDTO, TEntity> dto)
        {
            throw new NotImplementedException();
        }

        public virtual IZDataBase ToData()
        {
            throw new NotImplementedException();
        }

        public virtual IZDTOBase<TEntityDTO, TEntity> ToDTO()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        #endregion Methods
    }
}
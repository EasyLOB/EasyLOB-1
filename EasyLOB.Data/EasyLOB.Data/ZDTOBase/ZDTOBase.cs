using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLOB.Data
{
    public abstract class ZDTOBase<TEntityDTO, TEntity> : IZDTOBase<TEntityDTO, TEntity>, IValidatableObject
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        public virtual string LookupText { get; set; }

        #endregion Properties

        #region Methods

        public virtual Func<TEntityDTO, TEntity> GetDataSelector()
        {
            throw new NotImplementedException();
        }

        public virtual Func<TEntity, TEntityDTO> GetDTOSelector()
        {
            throw new NotImplementedException();
        }

        public virtual void FromData(IZDataBase data)
        {
            throw new NotImplementedException();
        }

        public virtual IZDataBase ToData()
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
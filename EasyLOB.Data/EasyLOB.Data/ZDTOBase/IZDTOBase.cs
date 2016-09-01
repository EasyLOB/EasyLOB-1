using System;

namespace EasyLOB.Data
{
    public interface IZDTOBase<TEntityDTO, TEntity>
    {
        #region Properties

        string LookupText { get; set; }

        #endregion Properties

        #region Methods

        Func<TEntityDTO, TEntity> GetDataSelector();

        Func<TEntity, TEntityDTO> GetDTOSelector();

        void FromData(IZDataBase data);

        IZDataBase ToData();

        #endregion Methods
    }
}
using System;

namespace EasyLOB.Data
{
    public interface IZViewBase<TEntityView, TEntityDTO, TEntity>
    {
        #region Properties

        string LookupText { get; set; }

        #endregion Properties

        #region Methods

        Func<TEntityView, TEntityDTO> GetDTOSelector();

        Func<TEntityDTO, TEntityView> GetViewSelector();

        void FromData(IZDataBase data);

        void FromDTO(IZDTOBase<TEntityDTO, TEntity> dto);

        IZDataBase ToData();

        IZDTOBase<TEntityDTO, TEntity> ToDTO();

        #endregion Methods
    }
}
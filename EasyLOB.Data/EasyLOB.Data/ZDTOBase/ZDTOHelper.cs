using System;
using System.Collections.Generic;

namespace EasyLOB.Data
{
    public static partial class ZDTOHelper<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public static List<TEntityDTO> ToDTOList(IEnumerable<TEntity> dataModels) // List<DataModel> -> List<DTO>
        {
            List<TEntityDTO> dtos = new List<TEntityDTO>();

            foreach (TEntity dataModel in dataModels)
            {
                dtos.Add((TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO), dataModel));
            }

            return dtos;
        }

        public static List<TEntity> ToDataList(IEnumerable<TEntityDTO> dtos) // List<DTO> -> List<DataModel>
        {
            List<TEntity> dataModels = new List<TEntity>();

            foreach (TEntityDTO dto in dtos)
            {
                dataModels.Add(dto.ToData() as TEntity);
            }

            return dataModels;
        }

        #endregion Methods
    }
}
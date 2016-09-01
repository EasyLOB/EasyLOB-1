using System;
using System.Collections.Generic;

namespace EasyLOB.Data
{
    public static partial class ZViewHelper<TEntityView, TEntityDTO, TEntity>
        where TEntityView : class, IZViewBase<TEntityView, TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public static List<TEntityView> ToViewList(IEnumerable<TEntityDTO> dtos) // List<DTO> -> List<ViewModel>
        {
            List<TEntityView> viewModels = new List<TEntityView>();

            foreach (TEntityDTO dto in dtos)
            {
                TEntityView view = (TEntityView)Activator.CreateInstance(typeof(TEntityView), dto);

                viewModels.Add(view);
            }

            return viewModels;
        }

        public static List<TEntityView> ToViewList(IEnumerable<TEntity> dataModels) // List<DataModel> -> List<ViewModel>
        {
            List<TEntityView> viewModels = new List<TEntityView>();

            foreach (TEntity dataModel in dataModels)
            {
                TEntityDTO dto = (TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO), dataModel);
                TEntityView view = (TEntityView)Activator.CreateInstance(typeof(TEntityView), dto);

                viewModels.Add(view);
            }

            return viewModels;
        }

        public static List<TEntityDTO> ToDTOList(IEnumerable<TEntityView> viewModels) // List<ViewModel> -> List<DTO>
        {
            List<TEntityDTO> dtos = new List<TEntityDTO>();

            foreach (TEntityView viewModel in viewModels)
            {
                dtos.Add(viewModel.ToDTO() as TEntityDTO);
            }

            return dtos;
        }

        public static List<TEntity> ToDataList(IEnumerable<TEntityView> viewModels) // List<ViewModel> -> List<DataModel>
        {
            List<TEntity> datas = new List<TEntity>();

            foreach (TEntityView viewModel in viewModels)
            {
                datas.Add((viewModel.ToDTO() as TEntityDTO).ToData() as TEntity);
            }

            return datas;
        }

        #endregion Methods
    }
}
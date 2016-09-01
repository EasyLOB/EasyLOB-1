using EasyLOB.Application;
using EasyLOB.Data;

namespace EasyLOB.Security.Application
{
    public interface ISecurityGenericApplicationDTO<TEntityDTO, TEntity> : IGenericApplicationDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
    }
}
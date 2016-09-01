using EasyLOB.Application;
using EasyLOB.Data;

namespace EasyLOB.AuditTrail.Application
{
    public interface IAuditTrailGenericApplicationDTO<TEntityDTO, TEntity> : IGenericApplicationDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
    }
}
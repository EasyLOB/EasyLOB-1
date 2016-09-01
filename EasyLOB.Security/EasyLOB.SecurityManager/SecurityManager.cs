using EasyLOB.Identity;
using EasyLOB.Library;
using EasyLOB.Persistence;
using EasyLOB.Security.Data;
using EasyLOB.Security.Persistence;
using System;
using System.Linq;

namespace EasyLOB.Security
{
    public class SecurityManager : ISecurityManager
    {
        #region Properties

        public ISecurityUnitOfWork UnitOfWork { get; }

        #endregion Properties

        #region Methods

        public SecurityManager(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ZIsSecurityOperations GetOperations(string activity)
        {
            ZIsSecurityOperations result = new ZIsSecurityOperations();
            result.Activity = activity;

            if (IdentityHelper.IsAdministrator)
            {
                result.IsSearch = true;
                result.IsCreate = true;
                result.IsRead = true;
                result.IsUpdate = true;
                result.IsDelete = true;
                result.IsExport = true;
                result.IsImport = true;
                result.IsExecute = true;

                return result;
            }

            if (!String.IsNullOrEmpty(activity))
            {
                string operationSearchAcronym = SecurityHelper.GetSecurityOperationAcronym(ZSecurityOperations.Search);
                string operationCreateAcronym = SecurityHelper.GetSecurityOperationAcronym(ZSecurityOperations.Create);
                string operationReadAcronym = SecurityHelper.GetSecurityOperationAcronym(ZSecurityOperations.Read);
                string operationUpdateAcronym = SecurityHelper.GetSecurityOperationAcronym(ZSecurityOperations.Update);
                string operationDeleteAcronym = SecurityHelper.GetSecurityOperationAcronym(ZSecurityOperations.Delete);

                IGenericRepository<ActivityRole> repositoryActivityRole = UnitOfWork.GetRepository<ActivityRole>();
                IGenericRepository<Activity> repositoryActivity = UnitOfWork.GetRepository<Activity>();
                IGenericRepository<UserRole> repositoryUserRole = UnitOfWork.GetRepository<UserRole>();

                IQueryable<ActivityRole> activityRoles =
                    from
                        ActivityRole in repositoryActivityRole.Query
                    from
                        Activity in repositoryActivity.Query
                    from
                        UserRole in repositoryUserRole.Query
                    where
                        ActivityRole.ActivityId == Activity.Id
                        && Activity.Name == activity
                        && ActivityRole.RoleId == UserRole.RoleId
                        && UserRole.UserId == IdentityHelper.UserId
                    select
                        ActivityRole;

                foreach (ActivityRole activityRole in activityRoles.ToList())
                {
                    string operations = activityRole.Operations.ToUpper();

                    result.IsSearch = result.IsSearch || operations.Contains(operationSearchAcronym);
                    result.IsCreate = result.IsCreate || operations.Contains(operationCreateAcronym);
                    result.IsRead = result.IsRead || operations.Contains(operationReadAcronym);
                    result.IsUpdate = result.IsUpdate || operations.Contains(operationUpdateAcronym);
                    result.IsDelete = result.IsDelete || operations.Contains(operationDeleteAcronym);
                }
            }

            return result;
        }

        public bool IsAuthorized(string activity, ZSecurityOperations operation)
        {
            if (IdentityHelper.IsAdministrator)
            {
                return true;
            }

            bool result = false;

            if (!String.IsNullOrEmpty(activity))
            {
                string operationAcronym = SecurityHelper.GetSecurityOperationAcronym(operation);

                IGenericRepository<ActivityRole> repositoryActivityRole = UnitOfWork.GetRepository<ActivityRole>();
                IGenericRepository<Activity> repositoryActivity = UnitOfWork.GetRepository<Activity>();
                IGenericRepository<UserRole> repositoryUserRole = UnitOfWork.GetRepository<UserRole>();

                IQueryable<ActivityRole> activityRoles =
                    from
                        ActivityRole in repositoryActivityRole.Query
                    from
                        Activity in repositoryActivity.Query
                    from
                        UserRole in repositoryUserRole.Query
                    where
                        ActivityRole.ActivityId == Activity.Id
                        && Activity.Name == activity
                        && ActivityRole.RoleId == UserRole.RoleId
                        && UserRole.UserId == IdentityHelper.UserId
                    select
                        ActivityRole;

                foreach (ActivityRole activityRole in activityRoles.ToList())
                {
                    if (activityRole.Operations.ToUpper().Contains(operationAcronym))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        public bool IsAuthorized(string activity, ZSecurityOperations operation, ZOperationResult operationResult)
        {
            bool result = IsAuthorized(activity, operation);
            
            if (!result)
            {
                operationResult.ErrorMessage = SecurityHelper.MessageNotAuthorized(activity, operation);
            }
            
            return result;
        }

        #endregion Methods
    }
}
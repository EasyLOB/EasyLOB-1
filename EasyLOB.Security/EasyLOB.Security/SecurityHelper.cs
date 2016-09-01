using EasyLOB.Activity.Resources;
using EasyLOB.Identity;
using EasyLOB.Library;
using System;

namespace EasyLOB.Security
{
    #region Types

    public enum ZSecurityOperations
    {
        Search,
        Create,
        Read,
        Update,
        Delete,
        Export,
        Import,
        Execute,
        None
    };

    #endregion Types

    public static partial class SecurityHelper
    {
        #region Properties

        //public static bool IsSecurity
        //{
        //    get
        //    {
        //        return LibraryHelper.AppSettings<bool>("EasyLOB.Security");
        //    }
        //}

        #endregion Properties

        #region Methods IsActivity

        public static bool IsSearch(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsSearch)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Search));
            }

            return result;
        }

        public static bool IsCreate(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsCreate)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Create));
            }

            return result;
        }

        public static bool IsRead(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsRead)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Read));
            }

            return result;
        }

        public static bool IsUpdate(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsUpdate)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Update));
            }

            return result;
        }

        public static bool IsDelete(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsDelete)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Delete));
            }

            return result;
        }

        public static bool IsExport(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsExport)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Export));
            }

            return result;
        }

        public static bool IsImport(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsImport)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Import));
            }

            return result;
        }

        public static bool IsExecute(ZIsSecurityOperations isSecurityOperations, ZOperationResult operationResult)
        {
            bool result = true;

            if (!isSecurityOperations.IsExecute)
            {
                result = false;
                operationResult.AddOperationError("", MessageNotAuthorized(isSecurityOperations.Activity, ZSecurityOperations.Delete));
            }

            return result;
        }

        #endregion Methods Is

        #region Methods IsSecurityOperation

        public static bool GetIsSecurityOperation(ZIsSecurityOperations isSecurityOperations, ZSecurityOperations operation)
        {
            bool result = false;

            switch (operation)
            {
                case ZSecurityOperations.Search:
                    result = isSecurityOperations.IsSearch;
                    break;

                case ZSecurityOperations.Create:
                    result = isSecurityOperations.IsCreate;
                    break;

                case ZSecurityOperations.Read:
                    result = isSecurityOperations.IsRead;
                    break;

                case ZSecurityOperations.Update:
                    result = isSecurityOperations.IsUpdate;
                    break;

                case ZSecurityOperations.Delete:
                    result = isSecurityOperations.IsDelete;
                    break;
            }

            return result;
        }

        public static bool GetIsSecurityOperationByAcronym(ZIsSecurityOperations isSecurityOperations, string acronym)
        {
            return GetIsSecurityOperation(isSecurityOperations, GetSecurityOperationByAcronym(acronym));
        }

        public static bool GetIsSecurityOperationByName(ZIsSecurityOperations isSecurityOperations, string name)
        {
            return GetIsSecurityOperation(isSecurityOperations, GetSecurityOperationByAcronym(name));
        }

        #endregion Methods IsSecurityOperation

        #region Methods Message

        public static string MessageAuthorized(string activity, ZSecurityOperations securityOperation)
        {
            return String.Format(SecurityActivityResources.Authorized,
                activity,
                SecurityHelper.GetSecurityOperationName(securityOperation),
                IdentityHelper.UserName);
        }

        public static string MessageNotAuthorized(string activity, ZSecurityOperations securityOperation)
        {
            return String.Format(SecurityActivityResources.NotAuthorized,
                activity,
                SecurityHelper.GetSecurityOperationName(securityOperation),
                IdentityHelper.UserName);
        }

        #endregion Methods Message

        #region Methods SecurityOperation

        public static string GetSecurityOperationAcronym(ZSecurityOperations securityOperation)
        {
            string result = "";

            try
            {
                int index = (int)securityOperation;
                result = SecurityDefaults.SecurityOperationsAcronyms[index];
            }
            catch
            {
            }

            return result;
        }

        public static ZSecurityOperations GetSecurityOperationByAcronym(string acronym)
        {
            ZSecurityOperations result = ZSecurityOperations.None;

            try
            {
                int index = Array.IndexOf(SecurityDefaults.SecurityOperationsAcronyms, acronym);
                if (index > 0)
                {
                    result = (ZSecurityOperations)index;
                }
            }
            catch
            {
            }

            return result;
        }

        public static ZSecurityOperations GetSecurityOperationByName(string name)
        {
            ZSecurityOperations result = ZSecurityOperations.None;

            try
            {
                int index = Array.IndexOf(SecurityDefaults.SecurityOperationsNames, name);
                if (index > 0)
                {
                    result = (ZSecurityOperations)index;
                }
            }
            catch
            {
            }

            return result;
        }

        public static string GetSecurityOperationName(ZSecurityOperations securityOperation)
        {
            string result = "";

            try
            {
                int index = (int)securityOperation;
                result = SecurityDefaults.SecurityOperationsNames[index];
            }
            catch
            {
            }

            return result;
        }

        #endregion Methods SecurityOperation
    }
}

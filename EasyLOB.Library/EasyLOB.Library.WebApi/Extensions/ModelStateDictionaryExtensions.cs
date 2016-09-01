using System;
using System.Web.Http.ModelBinding;

/*
ModelState.AddModelError(String.Empty, "Error");
ModelState.AddModelError("Property","Error");

ZOperationResult result = new ZOperationResult();
result.AddValidationResult("Error");
ModelState.AddOperationResults(result, "Entity")
*/

namespace EasyLOB.Library.WebApi
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddOperationResults(this ModelStateDictionary modelStateDictionary,
            ZOperationResult operationResult, string entity)
        {
            entity = String.IsNullOrEmpty(entity) ? "" : entity + ".";

            if (!String.IsNullOrEmpty(operationResult.ErrorMessage))
            {
                modelStateDictionary.AddModelError(String.Empty, operationResult.ErrorMessage);
            }

            foreach (ZOperationError operationError in operationResult.OperationErrors)
            {
                bool members = false;

                foreach (string member in operationError.ErrorMembers)
                {
                    members = true;
                    modelStateDictionary.AddModelError(entity + member, operationError.ErrorMessage);
                }

                if (!members)
                {
                    modelStateDictionary.AddModelError(String.Empty, operationError.ErrorMessage);
                }
            }
        }
    }
}
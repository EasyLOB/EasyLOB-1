using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyLOB.Library.WebApi
{
    public class OperationResultResponseException : HttpResponseException
    {
        #region Methods

        public OperationResultResponseException(HttpRequestMessage request, ZOperationResult operationResult)
            : base(request.CreateResponse<ZOperationResult>(HttpStatusCode.BadRequest, operationResult))
        {
        }

        #endregion Methods
    }
}
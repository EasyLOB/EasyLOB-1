using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyLOB.Library.WebApi
{
    public class ODataHelper
    {
        public static HttpResponseException OperationResultResponseException(HttpRequestMessage request, ZOperationResult operationResult)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                //Formatting = Formatting.Indented
                Formatting = Formatting.None
            };

            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(operationResult, settings)),
                StatusCode = HttpStatusCode.BadRequest
            };

            return new HttpResponseException(response);
        }
    }
}
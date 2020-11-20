using System.Net;
using Microsoft.AspNetCore.Http;

namespace Kaiheila.Cqhttp.Utils
{
    public static class CommunicationHelper
    {
        public static void SetStatusCode(this HttpResponse httpResponse, HttpStatusCode httpStatusCode) =>
            httpResponse.StatusCode = (int)httpStatusCode;
    }
}

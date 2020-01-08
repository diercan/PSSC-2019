using Microsoft.AspNetCore.Http;

namespace FrBaschet.API.Extensions
{
    public static class RequestExtension
    {
        public static string GetBaseUrl(this HttpRequest httpRequest)
        {
            return $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}";
        }
    }
}
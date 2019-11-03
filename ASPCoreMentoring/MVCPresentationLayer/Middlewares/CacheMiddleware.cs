using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MVCPresentationLayer.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Middlewares
{
    public class CacheMiddleware
    {
        private const string ControllerName = "Category";
        private const string ActionName = "Picture";
        private const string ContentType = "image/bmp";
        private const string HttpVerb = "GET";

        private readonly RequestDelegate _next;

        public CacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICache cacheService, IOptions<CacheSection> config)
        {
            byte[] cachedData = null;

            cacheService.ExpirationTime = config.Value.ExpirationTime;
            cacheService.MaxCount = config.Value.MaxCount;
            cacheService.PathDirectory = config.Value.PathDirectory;

            var action = context.GetRouteData().Values["action"].ToString();
            var controller = context.GetRouteData().Values["controller"].ToString();
            var id = context.GetRouteData().Values["id"]?.ToString();
            var verb = context.Request.Method;

            var isCachedRequest = (verb == HttpVerb && controller == ControllerName && action == ActionName) == true;

            if (isCachedRequest)
            {
                cachedData = await cacheService.GetPictureByNameAsync(id.ToString());
                if (cachedData != null)
                {
                    context.Response.ContentType = ContentType;
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    await context.Response.Body.WriteAsync(cachedData);
                }
                else
                {
                    var bufferedData = await CaptureResponseAsync(context);
                    if (context.Response.ContentType == ContentType
                        && context.Response.StatusCode == (int)HttpStatusCode.OK
                        && bufferedData != null)
                    {
                        await cacheService.SetPictureByNameAsync(id.ToString(), bufferedData);
                    }
                }
            }
            else { await _next.Invoke(context); }
        }

        private async Task<byte[]> CaptureResponseAsync(HttpContext context)
        {
            var responseStream = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    context.Response.Body = memoryStream;
                    await _next.Invoke(context);
                }
                finally
                {
                    context.Response.Body = responseStream;
                }

                if (memoryStream.Length == 0)
                    return null;

                var bufferedData = memoryStream.ToArray();
                responseStream.Write(bufferedData, 0, bufferedData.Length);

                return bufferedData;
            }
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vic.Core.Utils.WebApi
{
    public class ApiLogMiddleware
    {
        private readonly RequestDelegate _next;

        private static ILogger<ApiLogMiddleware> _logger;

        private ApiLog apiLog;

        public ApiLogMiddleware(RequestDelegate next, ILogger<ApiLogMiddleware> loggerFactory)
        {
            _next = next;
            _logger = loggerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            apiLog = new ApiLog();
            HttpRequest request = context.Request;
            apiLog.Url = request.Path.ToString();
            apiLog.Headers = request.Headers.ToDictionary(k => k.Key, v => string.Join(";", v.Value.ToList()));
            apiLog.Method = request.Method;
            apiLog.ExcuteStartTime = DateTime.Now;
            apiLog.IP = "";


            using (StreamReader reader = new StreamReader(request.Body))
            {
                apiLog.RequestBody = reader.ReadToEnd();
            }

            context.Response.OnCompleted(ResponseCompletedCallback, context);
            await _next(context);
        }

        private Task ResponseCompletedCallback(object obj)
        {
            apiLog.ExcuteEndTime = DateTime.Now;

            _logger.LogError($"apiLog: {apiLog.ToString()}");
            return Task.FromResult(0);
        }

        private Task ResponseUnAuthorityCompletedCallback(object obj)
        {
            apiLog.ExcuteEndTime = DateTime.Now;

            _logger.LogError($"非法请求 apiLog:{apiLog.ToString()}");
            return Task.FromResult(0);
        }
    }

    public static class ApiLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiLogMiddleware>();
        }
    }
}

//using Application.Serializer;
//using System.Globalization;
//using Application.Common;
//using Newtonsoft.Json;
//using Domain.Common;

//namespace SystemTicketing.EXpectionMiddleWare
//{
//    public class ExceptionHandlingMiddleware(RequestDelegate next)
//    {
//        private readonly RequestDelegate _next = next;

//        public async Task Invoke(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {


//                bool isApiRequest = context.Request.Path.StartsWithSegments("/api");

//                var response = new ApiResponse(false, ex.Message, StatusCodes.Status500InternalServerError, ex.Data?[ApiConsts.ExceptionKey]);

//                if (ex.Data is not null)
//                {
//                    var statusCode = ex.Data[ApiConsts.StatusCodeKey];
//                    if (statusCode is not null)
//                        response.Code = (int)statusCode;

//                    var data = ex.Data[ApiConsts.DataKey];
//                    if (data is not null)
//                        response.Data = data;

//                }

//                if (!isApiRequest)
//                {
//                    await _next(context);
//                }

//                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//                context.Response.ContentType = "application/json";
//                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));



//                //await ExceptionHandlingAsync(context, ex);
//            }
//        }





using Application.Common;
using Application.Serializer;
using Domain.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SystemTicketing.EXpectionMiddleWare
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // تجاهل طلبات Swagger
                if (context.Request.Path.StartsWithSegments("/swagger"))
                {
                    await _next(context);
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                var response = new ApiResponse(
                    Result: false,
                    Message: ex.Message,
                    Code: StatusCodes.Status500InternalServerError,
                    Data: ex.Data?[ApiConsts.ExceptionKey]);

                // معالجة أنواع خاصة من الاستثناءات
                switch (ex)
                {
                    case UnauthorizedAccessException:
                        response.Code = StatusCodes.Status401Unauthorized;
                        break;
                    case KeyNotFoundException:
                        response.Code = StatusCodes.Status404NotFound;
                        break;
                    case ValidationException:
                        response.Code = StatusCodes.Status400BadRequest;
                        break;
                }

                // معالجة البيانات الإضافية من الاستثناء
                if (ex.Data is not null)
                {
                    if (ex.Data[ApiConsts.StatusCodeKey] is int statusCode)
                        response.Code = statusCode;

                    if (ex.Data[ApiConsts.DataKey] is object data)
                        response.Data = data;
                }

                context.Response.StatusCode = response.Code;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}













































        //private async Task ExceptionHandlingAsync(HttpContext context ,Exception ex)
        //{
        //    bool isApiRequest = context.Request.Path.StartsWithSegments("/api");

        //    var response = new ApiResponse(false, ex.Message, StatusCodes.Status500InternalServerError, ex.Data?[ApiConsts.ExceptionKey]);

        //    if (ex.Data is not null)
        //    {
        //        var statusCode = ex.Data[ApiConsts.StatusCodeKey];
        //        if (statusCode is not null)
        //            response.Code = (int)statusCode;

        //        var data = ex.Data[ApiConsts.DataKey];
        //        if (data is not null)
        //            response.Data = data;

        //    }

        //    if (!isApiRequest)
        //    {
        //        await _next(context);
        //    }

        //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //    context.Response.ContentType = "application/json";
        //   await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        //}

//}
//}

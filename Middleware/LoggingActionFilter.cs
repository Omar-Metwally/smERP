using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace smERP.Middleware
{
    public class LoggingActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerBase;
            var action = context.ActionDescriptor.DisplayName;
            var request = context.HttpContext.Request;

            _logger.LogInformation(
                "Entering {Controller}.{Action} - Method: {Method}, Path: {Path}, Query: {@Query}, Body: {@Body}",
                controller?.GetType().Name,
                action,
                request.Method,
                request.Path,
                request.Query,
                GetRequestBody(request)
            );
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as ControllerBase;
            var action = context.ActionDescriptor.DisplayName;
            var response = context.HttpContext.Response;

            _logger.LogInformation(
                "Exiting {Controller}.{Action} - StatusCode: {StatusCode}, Result: {@Result}",
                controller?.GetType().Name,
                action,
                response.StatusCode,
                context.Result
            );
        }

        private static object GetRequestBody(HttpRequest request)
        {
            if (request.ContentLength == null || request.ContentLength == 0)
            {
                return null;
            }

            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = reader.ReadToEndAsync().Result;
            request.Body.Position = 0;

            return body;
        }
    }
}

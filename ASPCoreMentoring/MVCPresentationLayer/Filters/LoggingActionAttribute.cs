using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MVCPresentationLayer.Configuration;
using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace MVCPresentationLayer.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LoggingActionAttribute : ActionFilterAttribute
    {
        private readonly IOptions<LoggingFilterSection> _config;
        private readonly ILogger<LoggingActionAttribute> _logger;

        public LoggingActionAttribute(IOptions<LoggingFilterSection> config, ILogger<LoggingActionAttribute> logger)
        {
            _config = config;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string arguments = null;

            if (_config.Value.IsEnable)
                arguments = string.Join("; ", context.ActionArguments.Select(a => $"{a.Key} : {a.Value}"));

            LogRouteData("OnActionExecuting", context.RouteData, arguments);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogRouteData("OnActionExecuted", context.RouteData);
            base.OnActionExecuted(context);
        }

        private void LogRouteData(string eventN, RouteData routeData, string arguments = null)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];

            var argString = string.Empty;

            if (!string.IsNullOrEmpty(arguments))
                argString = $", arguments: {arguments}";

            var message = $"Event :{eventN} , controller: {controller} , action: {action}{argString}";

            _logger.LogInformation(message);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcFilters.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		private readonly ILogger<ExceptionFilter> _logger;

		public ExceptionFilter(ILogger<ExceptionFilter> logger)
		{
			_logger = logger;
		}
		public void OnException(ExceptionContext context)
		{
			var exceptionType = context.Exception.GetType();
			var controllerName = context.RouteData.Values["controller"].ToString();
			var actionName = context.RouteData.Values["action"].ToString();

			if (exceptionType == typeof(ArgumentNullException))
			{
				_logger.LogError($"Argument Null Exception in {controllerName}/{actionName}: {context.Exception.Message}");
				context.Result = new BadRequestObjectResult(
					new { error = "Invalid input provided. Please check your data and try again." });
			}
			else if (exceptionType == typeof(UnauthorizedAccessException))
			{
				_logger.LogWarning($"Unauthorized Access in {controllerName}/{actionName}: {context.Exception.Message}");
				//context.Result = new UnauthorizedResult();
				var problemDetails = new {
					Status = StatusCodes.Status401Unauthorized,
					Title = "Unauthorized",
					Detail = context.Exception.Message,
					Instance = context.HttpContext.Request.Path
				};

				context.Result = new ObjectResult(problemDetails)
				{
					StatusCode = StatusCodes.Status401Unauthorized,
					ContentTypes = { "application/problem+json" }
				};
			}
			else
			{
				_logger.LogError($"Unhandled exception in {controllerName}/{actionName}: {context.Exception}");
				context.Result = new StatusCodeResult(500);
			}

			context.ExceptionHandled = true;
		}
	}
}

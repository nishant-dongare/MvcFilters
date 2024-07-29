using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MvcFilters.Filters
{ 
	public class CustomAuthorizationFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new UnauthorizedResult();
			}
		}
	}

	public class CustomResourceFilter : IResourceFilter
	{
		public void OnResourceExecuting(ResourceExecutingContext context)
		{
			// Code executed before model binding
			Console.WriteLine("Resource executing");
		}

		public void OnResourceExecuted(ResourceExecutedContext context)
		{
			// Code executed after the action method
			Console.WriteLine("Resource executed");
		}
	}

	public class CustomActionFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			// Code executed before the action method
			Console.WriteLine("Action executing");
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			// Code executed after the action method
			Console.WriteLine("Action executed");
		}
	}

	public class CustomResultFilter : IResultFilter
	{
		public void OnResultExecuting(ResultExecutingContext context)
		{
			// Code executed before the result
			Console.WriteLine("Result executing");
		}

		public void OnResultExecuted(ResultExecutedContext context)
		{
			// Code executed after the result
			Console.WriteLine("Result executed");
		}
	}


}

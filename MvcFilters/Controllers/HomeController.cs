using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcFilters.Filters;
using MvcFilters.Models;

namespace MvcFilters.Controllers
{
	//[TypeFilter(typeof(CustomAuthorizationFilter))]
	//[TypeFilter(typeof(CustomResourceFilter))]
	[TypeFilter(typeof(CustomActionFilter))]
	//[TypeFilter(typeof(CustomResultFilter))]
	[TypeFilter(typeof(ExceptionFilter))]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Route("Home/Unauthorized/{id}")]
		public IActionResult UnauthorizedFilterException(int id)
		{
			if (id < 5)
			{
				throw new UnauthorizedAccessException($"Access denied for id: {id}");
			}

			// If id is 5 or greater, return a successful response
			return Ok($"Access granted for id: {id}");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

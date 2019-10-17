using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer.Controllers
{
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            _logger.LogError(error, $"Error while processing request with Activity.Current.Id: {requestId}");
            var errorViewModel = new ErrorViewModel { RequestId = requestId };

            return View(errorViewModel);
        }
    }
}

using AbpAspNetCoreDemo.Models;
using AbpAspNetCoreDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AbpAspNetCoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHelloAbpService _helloAbpService;

        public HomeController(ILogger<HomeController> logger, IHelloAbpService helloAbpService)
        {
            _logger = logger;
            _helloAbpService = helloAbpService;
        }

        public IActionResult Index()
        {
            ViewBag.HelloString = _helloAbpService.GetHelloString();

            return View();
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

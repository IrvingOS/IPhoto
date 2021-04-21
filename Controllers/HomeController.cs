using IPhoto.Models;
using IPhoto.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace IPhoto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPhotoService _iPhotoService;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IPhotoService photoService)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._iPhotoService = photoService;
        }

        public IActionResult Index()
        {
            ViewBag.PhotoList = _iPhotoService.ListAsync().Result;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IPhoto.Models;

namespace IPhoto.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Collection()
        {

            return View();
        }
    }
}

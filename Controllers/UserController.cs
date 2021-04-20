using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IPhoto.Models;
using System.ComponentModel.DataAnnotations;
using static IPhoto.Views.User.IndexModel;

namespace IPhoto.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public class InputModel
        {
            [Required]
            [Display(Name = "Title")]
            public string Title { get; set; }
            
            [Required]
            public string FileId { get; set; }
        }

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

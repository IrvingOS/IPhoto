using IPhoto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web;

namespace IPhoto.Controllers
{
    public class FileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IActionResult Upload ()
        {
            List<string> filenames = new List<string>();
            var _directory = System.AppDomain.CurrentDomain.BaseDirectory + "/Photos";
            
            return null;
        }
    }
}

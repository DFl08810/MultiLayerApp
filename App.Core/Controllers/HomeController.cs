using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Core.Models;
using Microsoft.AspNetCore.Identity;
using IdentityAuthLib.Models;
using App.Core.AuthInfrastructure;

namespace App.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,
                                UserManager<IdentityAuthLib.Models.User> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var AuthorizedUser = await _userManager.GetUserAsync(User);
            //initialize configuration object for view logic
            var viewConfiguration = new ViewConfigModel();

            if (AuthorizedUser == null)
            {
                viewConfiguration.IsSignedIn = false;
            }
            else
            {
                //if role is user, flag roles in config model
                if (User.IsInRole(Role.User))
                {
                    viewConfiguration.IsUser = true;
                    viewConfiguration.IsAdmin = false;
                }
                //if role is admin, reverse logic
                if (User.IsInRole(Role.Admin))
                {
                    viewConfiguration.IsUser = false;
                    viewConfiguration.IsAdmin = true;
                }

            }

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

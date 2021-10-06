using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityAuthLib.Models;
using App.Core.AuthInfrastructure;
using Microsoft.AspNetCore.Authorization;

namespace App.Core.Controllers
{
    //[Authorize(Roles=Role.Admin)]
    //Authorization tag restricts access to controllers and methods
    public class AppManagerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppManagerController(UserManager<IdentityAuthLib.Models.User> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        //Call this to generate default admin and roles
        public async Task<IActionResult> Index()
        {
            await CreateRoles();
            await CreateUsers();
            
            return View();
        }


        //Check if roles exist and creates them if not
        private async Task CreateRoles()
        {
            bool userCheck = await _roleManager.RoleExistsAsync(Role.User);
            if(!userCheck)
            {
                var role = new IdentityRole();
                role.Name = Role.User;
                await _roleManager.CreateAsync(role);
            }

            bool adminCheck = await _roleManager.RoleExistsAsync(Role.User);
            if (!adminCheck)
            {
                var roleAdmin = new IdentityRole();
                roleAdmin.Name = Role.Admin;
                await _roleManager.CreateAsync(roleAdmin);
            }
        }

        //Creates default admin account
        private async Task CreateUsers()
        { 
            var user = new User();
            user.UserName = "admin";
            user.Email = "admin@admin.com";
            user.EmailConfirmed = true;

            string password = "Password1234*";
            IdentityResult chkUser = await _userManager.CreateAsync(user, password);

            //created account is assigned with role
            if (chkUser.Succeeded)
            {
                var result1 = await _userManager.AddToRoleAsync(user, "Admin");
            }

        }

    }
}

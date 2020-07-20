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
    [Authorize(Roles=Role.Admin)]
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

        public async Task<IActionResult> Index()
        {
            //await CreateRolesandUsers();
            await CreateRoles();
            await CreateUsers();
            
            return View();
        }


        private async Task CreateRolesandUsers()
        {
            bool adminRole = await _roleManager.RoleExistsAsync("Admin");
            if (!adminRole)
            {
                var role = new IdentityRole();
                role.Name = Role.Admin;
                await _roleManager.CreateAsync(role);


                //string userPWD = "somepassword";

                //IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                //if (chkUser.Succeeded)
                //{
                //    var result1 = await _userManager.AddToRoleAsync(user, Role.Admin);
                //}
            }
        }

        private async Task CreateRoles()
        {
            var role = new IdentityRole();
            role.Name = Role.User;
            await _roleManager.CreateAsync(role);

            var roleAdmin = new IdentityRole();
            roleAdmin.Name = Role.Admin;
            await _roleManager.CreateAsync(roleAdmin);
        }

        private async Task CreateUsers()
        {
            var user = new User();
            user.UserName = "admin";
            user.Email = "admin@admin.com";
            user.EmailConfirmed = true;
            
            string password = "Password1234*";
            IdentityResult chkUser = await _userManager.CreateAsync(user, password);


            if (chkUser.Succeeded)
            {
                var result1 = await _userManager.AddToRoleAsync(user, "Admin");
            }

        }

    }
}

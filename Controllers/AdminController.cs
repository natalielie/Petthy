using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petthy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Controllers
{
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> manager)
        {
            _roleManager = manager;
        }

        public async Task<IActionResult> GetRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "ADMIN" });
            return View(await _roleManager.Roles.ToListAsync());
        }
    }
}

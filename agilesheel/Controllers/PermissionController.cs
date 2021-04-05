﻿using agilesheel.Constants;
using agilesheel.Helpers;
using agilesheel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index(string roleId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();

            // Define permissions from Constants/Roles.cs
            allPermissions.GetPermissions(typeof(Permissions.Tickets), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Movies), roleId);
            allPermissions.GetPermissions(typeof(Permissions.TextBar), roleId);
            
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();

            // Check if user has permissons en select those
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }

            model.RoleClaims = allPermissions;
            return View(model);
        }

        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();

            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }

            return RedirectToAction("Index", new { roleId = model.RoleId });
        }
    }
}

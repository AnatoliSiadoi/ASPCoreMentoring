using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPresentationLayer.Models.Administrator;

namespace MVCPresentationLayer.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdministratorController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> Index()
        {
            List<IdentityUserViewModel> users = null;
            var identityUsers = await  _userManager.Users.ToListAsync();
            if(identityUsers != null)
            {
                users = ConvertIdentityUserToViewModel(identityUsers);
                return View(users);
            }
            
            return View(users);
        }

        private List<IdentityUserViewModel> ConvertIdentityUserToViewModel(List<IdentityUser> identityUsers)
        {
            var result = identityUsers.Select(s=> new IdentityUserViewModel
            {
                PhoneNumber = s.PhoneNumber,
                ConcurrencyStamp = s.ConcurrencyStamp,
                SecurityStamp = s.SecurityStamp,
                PasswordHash = s.PasswordHash,
                NormalizedEmail = s.NormalizedEmail,
                Email = s.Email,
                UserName = s.UserName
            });

            return result.ToList();
        }
    }
}
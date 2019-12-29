using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Identity.Data
{
    public class SuperUserInitializer
    {
        private const string SuperUserEmail = "Administrator@mail.com";
        private const string SuperUserPwd = "Administrator@12345";

        private readonly UserManager<IdentityUser> _userManager;

        public SuperUserInitializer(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Add()
        {
            var user = _userManager.Users.FirstOrDefault(i => i.UserName == SuperUserEmail);

            if (user == null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "administrator")
                };

                var newUser = new IdentityUser()
                {
                    UserName = SuperUserEmail,
                    Email = SuperUserEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser, SuperUserPwd);
                await _userManager.AddClaimsAsync(newUser, claims);
            }
        }
    }
}

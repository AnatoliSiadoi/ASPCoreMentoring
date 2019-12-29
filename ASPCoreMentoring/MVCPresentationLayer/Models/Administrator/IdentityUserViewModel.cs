using Microsoft.AspNetCore.Identity;

namespace MVCPresentationLayer.Models.Administrator
{
    public class IdentityUserViewModel
    {
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public string NormalizedEmail { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}

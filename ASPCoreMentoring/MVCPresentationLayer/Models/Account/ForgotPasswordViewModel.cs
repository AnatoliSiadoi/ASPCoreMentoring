using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jobApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "FromEmailRequired")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "FromEmailRequired")]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resources))]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "PassRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "pass", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Resources))]
        public bool RememberMe { get; set; }
        public Job job { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "FromEmailRequired")]
        [EmailAddress]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "PassRequired")]
        [StringLength(100,MinimumLength = 6,
        ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "LongPass100")]
        [DataType(DataType.Password)]
        [Display(Name = "pass", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirmPass", ResourceType = typeof(Resources.Resources))]
        [Compare("Password",
            ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "ConfirmPassRequired")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

public class LoginModel {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}

public class RegisterModel {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Email Address")]
    public string EmailAddress { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Confirm email address")]
    [Compare("EmailAddress", ErrorMessage = "The email address and confirmation email address do not match.")]
    public string ConfirmEmailAddress { get; set; }
}

public class PasswordRecovery {
    public string EmailAddress { get; set; }
}
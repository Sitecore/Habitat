namespace Habitat.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Habitat.Accounts.Attributes;

  public class RegistrationInfo
  {
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "The email address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Login")]
    [Required(ErrorMessage = "The login is required")]
    public string UserName { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "The password is required")]
    [PasswordMinLength(ErrorMessage = "Minimum password length is {1}")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Confirm password")]   
    [Required(ErrorMessage = "The password is required")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
  }
}
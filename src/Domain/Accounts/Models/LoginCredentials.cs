namespace Habitat.Accounts.Models
{
  public class LoginCredentials
  {
    public string UserName { get; set; }

    public string Email { get; set; }

    public string ReturnUrl { get; set; }

    public string Password { get; set; }
  }
}
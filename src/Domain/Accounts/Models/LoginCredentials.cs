namespace Habitat.Accounts.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;

  public class LoginCredentials
  {
    public string UserName { get; set; }

    public string Email { get; set; }

    public string ReturnUrl { get; set; }

    public string Password { get; set; }
  }
}
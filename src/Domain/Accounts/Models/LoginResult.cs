using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Accounts.Models
{
  public class LoginResult
  {
    public string ValidationMessage { get; set; }

    public bool IsAuthenticated { get; set; }
  }
}
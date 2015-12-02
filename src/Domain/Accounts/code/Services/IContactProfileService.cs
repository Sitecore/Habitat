using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Accounts.Services
{
  using Habitat.Accounts.Models;

  public interface IContactProfileService
  {
    void SetPreferredEmail(string email);

    void SetPreferredPhoneNumber(string phoneNumber);

    void SetProfile(EditProfile profile);
  }
}
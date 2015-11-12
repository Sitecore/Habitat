namespace Habitat.Accounts.Models
{
  using System.Collections.Generic;

  public class EditProfile
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Interest { get; set; }

    public IEnumerable<string> InterestTypes { get; set; }
  }
}
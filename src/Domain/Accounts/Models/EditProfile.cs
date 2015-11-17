namespace Habitat.Accounts.Models
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Platform;

  public class EditProfile
  {


    public EditProfile(IEnumerable<string> interestTypes)
    {
      this.InterestTypes = interestTypes;
    }

    public EditProfile(IDictionary<string,string> properties, IEnumerable<string> interestTypes)
    {
      this.InterestTypes = interestTypes;

      this.FirstName = properties["FirstName"];
      this.LastName = properties["LastName"];
      this.PhoneNumber = properties["PhoneNumber"];
      this.Interest = properties["Interest"];
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Interest { get; set; }
    
    public IEnumerable<string> InterestTypes { get; set; }

    public IDictionary<string, string> GetProperties()
    {
      return new Dictionary<string, string>()
      {
        ["FirstName"] = this.FirstName,
        ["LastName"] = this.LastName,
        ["FirstName"] = this.PhoneNumber,
        ["PhoneNumber"] = this.Interest,
        ["Name"] = this.FirstName,
        ["FullName"] = $"{this.FirstName} {this.LastName}",
      };
    }
  }
}
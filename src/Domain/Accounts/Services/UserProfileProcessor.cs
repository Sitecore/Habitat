namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Web.Mvc;
  using Habitat.Accounts.Texts;

  public class UserProfileProcessor : IProfileProcessor
  {
    private readonly IEnumerable<string> interests;

    public UserProfileProcessor(): this(new ProfileSettingsService())
    {
    }

    public UserProfileProcessor(IProfileSettingsService profileSettingsService)
    {
      this.interests = profileSettingsService.GetInterests();
    }


    public virtual ModelStateDictionary Validate(IDictionary<string, string> properties)
    {
      var modelState = new ModelStateDictionary();

      if (properties.ContainsKey("PhoneNumber") && !string.IsNullOrEmpty(properties["PhoneNumber"]))
      {
        if (!Regex.IsMatch(properties["PhoneNumber"], "pattern"))
        {
          modelState.AddModelError("PhoneNumber", Errors.PhoneNumberFormat);
        }
      }

      if (properties.ContainsKey("Interest"))
      {
        if (!this.interests.Contains(properties["Interest"]))
        {
          modelState.AddModelError("PhoneNumber", Errors.WrongInterest);
        }
      }

      return modelState;
    }

    public virtual IDictionary<string, string> Process(IDictionary<string, string> properties)
    {
      if (properties.ContainsKey("FirstName"))
      {
        properties.Add("Name", properties["FirstName"]);

        if (properties.ContainsKey("LastName"))
        {
          properties.Add("FullName", $"{properties["FirstName"]} {properties["LastName"]}");
        }
      }

      return properties;
    }
  }
}
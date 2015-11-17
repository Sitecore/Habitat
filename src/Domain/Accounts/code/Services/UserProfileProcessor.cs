namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using Habitat.Accounts.Models;
  using Habitat.Accounts.Texts;
  using Sitecore.Diagnostics;

  public class UserProfileProcessor : IProfileProcessor
  {
    private readonly IProfileSettingsService profileSettingsService;
    
    public UserProfileProcessor() : this(new ProfileSettingsService())
    {
    }

    public UserProfileProcessor(IProfileSettingsService profileSettingsService)
    {
      this.profileSettingsService = profileSettingsService;
    }

    public virtual IDictionary<string, string> GetProperties(object profileModel)
    {
      var model = profileModel as EditProfile;
      Assert.IsNotNull(model, "Can't convert profile model to EditProfile type");

      return new Dictionary<string, string>()
      {
        ["FirstName"] = model.FirstName,
        ["LastName"] = model.LastName,
        ["PhoneNumber"] = model.PhoneNumber,
        ["Interest"] = model.Interest,
        ["Name"] = model.FirstName,
        ["FullName"] = $"{model.FirstName} {model.LastName}",
      };
    }

    public virtual object GetModel(IDictionary<string, string> properties)
    {
      var model = new EditProfile();
      if (properties.ContainsKey("FirstName"))
      {
        model.FirstName = properties["FirstName"];
      }
      if (properties.ContainsKey("LastName"))
      {
        model.FirstName = properties["LastName"];
      }
      if (properties.ContainsKey("PhoneNumber"))
      {
        model.FirstName = properties["PhoneNumber"];
      }
      if (properties.ContainsKey("Interest"))
      {
        model.FirstName = properties["Interest"];
      }

      model.InterestTypes = this.profileSettingsService.GetInterests();

      return model;
    }

    public IEnumerable<ValidationResult> ValidateModel(object profileModel)
    {
      var validationResults = new List<ValidationResult>();

      var model = profileModel as EditProfile;
      Assert.IsNotNull(model, "Can't convert profile model to EditProfile type");

      if (!this.profileSettingsService.GetInterests().Contains(model.Interest))
      {
        validationResults.Add(new ValidationResult(Errors.WrongInterest,new [] {"Interest"}));
      }

      return validationResults;
    }
  }
}
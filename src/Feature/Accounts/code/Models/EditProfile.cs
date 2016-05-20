namespace Sitecore.Feature.Accounts.Models
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using Sitecore.Feature.Accounts.Texts;

  public class EditProfile
  {
    [Display(Name = nameof(Captions.FirstName), ResourceType = typeof(Captions))]
    public string FirstName { get; set; }

    [Display(Name = nameof(Captions.LastName), ResourceType = typeof(Captions))]
    public string LastName { get; set; }

    [Display(Name = nameof(Captions.PhoneNumber), ResourceType = typeof(Captions))]
    [RegularExpression(@"^\+?\d*(\(\d+\)-?)?\d+(-?\d+)+$", ErrorMessageResourceName = nameof(Errors.PhoneNumberFormat), ErrorMessageResourceType = typeof(Errors))]
    [MaxLength(20, ErrorMessageResourceName = nameof(Errors.MaxLength), ErrorMessageResourceType = typeof(Errors))]
    public string PhoneNumber { get; set; }

    [Display(Name = nameof(Captions.Interests), ResourceType = typeof(Captions))]
    public string Interest { get; set; }

    public IEnumerable<string> InterestTypes { get; set; }
  }
}
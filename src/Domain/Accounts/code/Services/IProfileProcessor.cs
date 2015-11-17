namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public interface IProfileProcessor
  {
    IDictionary<string, string> GetProperties(object profileModel);
    object GetModel(IDictionary<string, string> properties);
    IEnumerable<ValidationResult> ValidateModel(object profileModel);
  }
}
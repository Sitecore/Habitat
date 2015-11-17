namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.Web.Mvc;

  public interface IProfileProcessor
  {
    ModelStateDictionary Validate(IDictionary<string, string> properties);

    IDictionary<string, string> Process(IDictionary<string, string> properties);
  }
}
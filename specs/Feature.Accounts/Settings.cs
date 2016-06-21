using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;

namespace Sitecore.Feature.Accounts.Specflow
{
  using System.Configuration;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;

  public class AccountSettings : BaseSettings
  {
    public string SearchContactUrl => BaseSettings.BaseUrl + "/sitecore/api/ao/proxy/contacts/search?match=";
    public string RolesTemplates => ConfigurationManager.AppSettings["rolesTemplates"];



  }
}
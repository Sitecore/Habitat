namespace Sitecore.Feature.Accounts.Specflow
{
  using Sitecore.Foundation.Common.Specflow.Infrastructure;

  public class AccountSettings : BaseSettings
  {
    public string SearchContactUrl => base.BaseUrl + "/sitecore/api/ao/proxy/contacts/search?match=";

  }
}
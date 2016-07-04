namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  using System.Collections.Generic;
  using System.ServiceModel;
  using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.UtfService;

  public class HelperWebServiceWrapper
  {
    private HelperWebServiceSoapClient client;

    public HelperWebServiceWrapper()
    {
      this.client = new HelperWebServiceSoapClient(new BasicHttpBinding(), new EndpointAddress(BaseSettings.UtfHelperService));
    }

    public string CreateItem(string itemIdOrPath, string parentIdOrPath, string templateIdOrPath)
    {
      return this.client.CreateItem(itemIdOrPath, parentIdOrPath, templateIdOrPath, BaseSettings.UserName, BaseSettings.Password, BaseSettings.ContextDatabase);
    }

    public string EditItem(string itemIdOrPath, string fieldName, string fieldValue)
    {
      return this.client.EditItem(itemIdOrPath, fieldName, fieldValue, BaseSettings.UserName, BaseSettings.Password, BaseSettings.ContextDatabase);
    }

    public IEnumerable<string> GetChildren(string parentIdOrPath, bool returnPath)
    {
      return this.client.GetChildren(parentIdOrPath, BaseSettings.ContextDatabase, returnPath);
    }

    public IEnumerable<string> GetChildren(string parentIdOrPath,Database database, bool returnPath)
    {
      return this.client.GetChildren(parentIdOrPath, database, returnPath);
    }

    public void DeleteItem(string itemIdOrPath, bool withException)
    {
      this.client.DeleteItem(itemIdOrPath, BaseSettings.ContextDatabase, withException);
    }

    public string GetItemFieldValue(string itemIdOrPath, string fieldName)
    {
      return this.client.GetItemFieldValue(itemIdOrPath, fieldName, BaseSettings.ContextDatabase);
    }

    public string ItemExistsByPath(string idOrPath)
    {
      return this.client.ItemExistsByPath(idOrPath, BaseSettings.ContextDatabase);
    }

    public void CreateUser(string username, string password, ArrayOfString roles, string mail)
    {
      this.client.CreateUser(username, password, roles, mail);
    }

    public bool UserOrRoleExists(string userOrRoleName, bool isUser)
    {
      return this.client.UserOrRoleExists(userOrRoleName, isUser);
    }
  }
}
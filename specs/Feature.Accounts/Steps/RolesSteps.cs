using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.InteropServices;
  using FluentAssertions;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Service_References.UtfService;
  using Sitecore.Foundation.Common.Specflow.Steps;
  using TechTalk.SpecFlow;
  using TechTalk.SpecFlow.Assist;

  [Binding]
  internal class RolesSteps
  {
    public AccountSettings Settings => new AccountSettings();

    [BeforeScenario(), Scope(Feature = "Serialise users and roles")]
    public void RolesSetup()
    {

      var database = Database.Master;
      var templateId = ContextExtensions.UtfService.CreateItem("tempTemplate"+DateTime.Now.Millisecond, "/sitecore/templates", "/sitecore/templates/System/Templates/Template", "sitecore\\admin", "b", database);
      //set base templates with applied habitat security
      ContextExtensions.UtfService.EditItem(templateId, "__base template", Settings.RolesTemplates, BaseSettings.UserName, BaseSettings.Password, database);

      var itemId = ContextExtensions.UtfService.CreateItem("tmpItem" + DateTime.Now.Millisecond, "/sitecore/content/Habitat", templateId, BaseSettings.UserName, BaseSettings.Password, database);

      //get all available field from base templates
      var fields = Settings.RolesTemplates.Split('|')
        .SelectMany(x => ContextExtensions.UtfService
          .GetChildren(x, database, false)
          .SelectMany(child => ContextExtensions.UtfService.GetChildren(child, database, false)));


      ScenarioContext.Current.Set(fields, "fields");
      ScenarioContext.Current.Set(itemId, "item");

   
      ContextExtensions.CleanupPool.Add(new TestCleanupAction()
      {
        ActionType = ActionType.DeleteItem,
        Payload = new EditFieldPayload() { Database = database, ItemIdOrPath = itemId }
      });
      ContextExtensions.CleanupPool.Add(new TestCleanupAction()
      {
        ActionType = ActionType.DeleteItem,
        Payload = new EditFieldPayload() { Database = database, ItemIdOrPath = templateId }
      });
    }

    [Then(@"Following roles available")]
    public void ThenFollowingRolesAvailable(Table table)
    {
      var expectedRoles = table.Rows.Select(x => x.Values.First());
      foreach (var role in expectedRoles)
      {
        ContextExtensions.UtfService.UserOrRoleExists(role, false).Should().BeTrue($"Role '{role}' is not exist");
      }
    }

    [Then(@"Following roles available for (.*) user")]
    public void ThenFollowingRolesAvailableForSitecoreAdminUser(string userName, Table table)
    {
      var expectedRoles = table.Rows.Select(x => x.Values.First());
      var rolesForUser = ContextExtensions.HelperService.GetRolesForUser(userName);

      foreach (var role in expectedRoles)
      {
        rolesForUser.Contains(role).Should().BeTrue($"Role '{role}' is not assigned to user '{userName}'");
      }
    }


    [Given(@"User (.*) with (.*) password and following roles created in Habitat")]
    public void GivenUserHabitatUserRolesWithUPasswordCreatedInHabitat(string username, string password, Table table)
    {
      var roles = new ArrayOfString() { };
      roles.AddRange(table.Rows.Select(x => x.Values.First()));

      ContextExtensions.UtfService.CreateUser(username, password, roles, "fake@domain.com");
      ContextExtensions.CleanupPool.Add(new TestCleanupAction()
      {
        ActionType = ActionType.RemoveUser,
        Payload = username
      });

    }








    [Then(@"(.*) has (\w*) (.*) access to all available item fields")]
    public void ThenAllAvailableItemFieldsAreDisabled(string username, string permission, string access)
    {
      SecurityRight rightAccess;
      AccessPermission accessPermission;

      var fields = ScenarioContext.Current.Get<IEnumerable<string>>("fields");
      var item = ScenarioContext.Current.Get<string>("item");

      var allow = string.IsNullOrWhiteSpace(permission);


      foreach (var field in fields)
      {
        ContextExtensions.HelperService.GetFieldSecurityRight("master", item, field, username, access).Should().Be(allow, "'{0}' should have {1} allow '{2}' right",field,permission,access);
      }
    }


    [Then(@"(.*) has (\w*) (.*) access to following item fields")]
    public void ThenHabitatUserRolesHasAllowFieldWriteAccessToFollowingItemFields(string username, string permission, string access, Table table)
    {

      var item = ScenarioContext.Current.Get<string>("item");

      var fields = ScenarioContext.Current.Get<IEnumerable<string>>("fields");

      var allow = string.IsNullOrWhiteSpace(permission);

      foreach (var field in table.Rows.Select(x => x.Values.First()))
      {
        ContextExtensions.HelperService.GetFieldSecurityRight("master", item, field, username, access).Should().Be(allow);
      }

    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  public class XDbSteps : AccountStepsBase
  {
    [Then(@"Contact collection Tags\.(.*)\.Values section for (.*) consist of")]
    public void ThenContactCollectionTags_Interests_ValuesSectionConsistOf(string tagName, string userName, Table table)
    {
      var tagsXdb = ContextExtensions.HelperService.GetContactTags($"extranet\\{userName}", tagName);

      var tags = table.Rows.First().Values;

      if (tags.FirstOrDefault() == null)
      {
        tagsXdb.Should().BeNullOrEmpty();
      }
      else
      {       
        tags.Should().BeEquivalentTo(tagsXdb);
      }
    }

    [Then(@"Contact (.*) has visit count (.*) and value (.*)")]
    public void ThenContactKovSitecore_NetHasVisitCountAndValue(string userName, int visits, int value)
    {
      var visit = ContextExtensions.HelperService.GetContactSystemInfo($"extranet\\{userName}");

      visit.VisitCount.Should().Be(visits);
      visit.Value.Should().Be(value);
    }

    [Then(@"Contact (.*) has FirstName equals (.*) and Surname equals (.*)")]
    public void ThenContactKovSitecore_NetHasFirstNameEqualsAndSurnameEquals(string userName, string firstName, string lastName)
    {
      var contactNames = ContextExtensions.HelperService.GetContactPersonalInfo($"extranet\\{userName}");

      if (firstName == "@empty")
      {
        contactNames.FirstName.Should().BeNullOrEmpty();
      }
      else
      {
        contactNames.FirstName.Should().Be(firstName);
      }

      if (lastName == "@empty")
      {
        contactNames.Surname.Should().BeNullOrEmpty();
      }
      else
      {
        contactNames.Surname.Should().Be(lastName);
      }      
    }

    [Then(@"Contact (.*) has PhoneNumber equals (.*)")]
    public void ThenContactKovSitecore_NetHasPhoneNumberEquals(string userName, string phoneNumber)
    {
      var phone = ContextExtensions.HelperService.GetContactPhone($"extranet\\{userName}", "Primary");

      if (phoneNumber == "@empty")
      {
        phone.Should().BeNullOrEmpty();
      }
      else
      {
        phone.Should().Be(phoneNumber);
      }     
    }

    [Then(@"Contact (.*) has SMTP emails equals (.*)")]
    public void ThenContactKovSitecore_NetHasSMTPEmailsEquals(string userName, string email)
    {
      var smtp = ContextExtensions.HelperService.GetContactEmail($"extranet\\{userName}", "Primary");
      smtp.Should().Be(email);
    }




  }
}

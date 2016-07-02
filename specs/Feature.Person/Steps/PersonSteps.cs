using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Specflow.Steps
{
  [Binding]
  public class PersonSteps : PersonStepsBase
  {
    [Then(@"Only following persons are shown")]
    public void ThenFollowingPersonsAreShown(Table table)
    {
      var persons = PersonLocators.EmployeesPerson.Select(x => x.FindElement(By.TagName("h4")));
      var expectedNames = table.Rows.SelectMany(x => x.Values);
      var actualNames = persons.Select(x => x.GetAttribute("innerText"));
      var expectedResultMessage = $"because persons {string.Join("|", expectedNames)} expected and {string.Join("|", actualNames)} present on page";
      expectedNames.All(n => actualNames.Any(x => x.Equals(n,StringComparison.InvariantCultureIgnoreCase))).Should().BeTrue(expectedResultMessage);
      expectedNames.Count().Should().Be(persons.Count(), expectedResultMessage);
    }

  }
}

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
      var names = table.Rows.Select(x => x.Values.First());
      names.All(n => persons.Any(x => x.GetAttribute("innerText").Equals(n,StringComparison.InvariantCultureIgnoreCase))).Should().BeTrue();
      names.Count().Should().Be(persons.Count());
    }

  }
}

using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.UtfService;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{


  [Binding]
  public class CommonGlobalSteps
  {
    [BeforeStep]
    public static void Timeout()
    {
#warning shitcode
      Thread.Sleep(3000);
    }
   
    public void CreateItem(string parentIdOrPath, string fieldName, string fieldValue)
    {
      ContextExtensions.UtfService.CreateItem(fieldName, parentIdOrPath, fieldValue);
    }


    public void EditItem(string idOrPath, string fieldName, string fieldValue)
    {
      ContextExtensions.UtfService.EditItem(idOrPath, fieldName, fieldValue);
    }

    public void ItemShouldExists(string idOrPath)
    {
      ContextExtensions.UtfService.ItemExistsByPath(idOrPath)
          .Should().NotBeEmpty($"Expected item {idOrPath} exists");
    }

    [When(@"Actor waits (.*) seconds")]
    public void WhenActorWaitsSeconds(int seconds)
    {
      Thread.Sleep(seconds);
    }



    [Given(@"Value set to item field")]
    [When(@"Value set to item field")]
    [Then(@"Value set to item field")]
    [Given(@"en is Selected on the following item")]
    [When(@"Value set to item field")]
    [When(@"The sitecore keyword has been selected")]
    public void GivenValueSetToItemField(IEnumerable<ItemFieldDefinition> fields)
    {
      foreach (var field in fields)
      {
        EditItem(field.ItemPath, field.FieldName, field.FieldValue);
      }
    }

    [Given(@"The following languages have been selected")]
    [When(@"The following languages have been selected")]
    public void WhenTheFollowingLanguagesHaveBeenSelected(IEnumerable<ItemFieldDefinition> items)
    {
      foreach (var item in items)
      {
        var languagePaths = item.FieldValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => $"{ItemService.LanguageRootPath}/{x}");
        var languageIds = languagePaths.Select(x => ContextExtensions.UtfService.ItemExistsByPath(x));
        var fieldValue = string.Join("|", languageIds);

        EditItem(item.ItemPath, item.FieldName, fieldValue);
      }

    }


    [Given(@"The following Metadata keywords are defined in Sitecore")]
    public void ItemsExists(IEnumerable<ItemFieldDefinition> items)
    {
      foreach (var field in items)
      {
        ItemShouldExists(field.ItemPath);
      }
    }

    [Given(@"Following languages defined in Sitecore")]
    [When(@"Admin opens following item")]
    public void GivenValueGetItemField(IEnumerable<ItemFieldDefinition> items)
    {
      foreach (var field in items)
      {
        ItemShouldExists(field.ItemPath);
      }
    }


    [Given(@"Following additional languages were defined in Sitecore")]
    public void GivenFollowingAdditionalLanguagesWereDefinedInSitecore(IEnumerable<LanguageModel> languages)
    {
      foreach (var language in languages)
      {
        if (LanguageProvider.LangaugeExists(language.ItemPath))
        {
          LanguageProvider.AddLanguage(language);
        }
      }
    }



    [Given(@"Admin create a new Metakeyword")]
    public void CreateItem(IEnumerable<ItemFieldDefinition> fields)
    {
      foreach (var field in fields)
      {
        CreateItem(field.ItemPath, field.FieldName, field.FieldValue);
      }
    }




    [Given(@"Actor Ends user visit")]
    [When(@"Actor Ends user visit")]
    public void WhenActorEndsUserVisit()
    {
      FeatureContext.Current.Get<IWebDriver>().Navigate().GoToUrl(BaseSettings.EndVisitUrl);
    }


    [Given(@"Control properties were defined for item")]
    public void GivenControlPropertiesWereDefinedForItem(IEnumerable<ControlProperties> properties)
    {
      foreach (var property in properties)
      {
        var renderingFieldValue = ContextExtensions.UtfService.GetItemFieldValue(property.ItemPath, "__Renderings");
        var renderingFieldXml = XDocument.Parse(renderingFieldValue);
        var templateId = ContextExtensions.HelperService.GetTemplateId(property.ItemPath);

        //find standard values to get base rendering value and update delta layout on target item
        var standardValuesPath = ContextExtensions.UtfService.GetItemFieldValue(templateId, "__Standard values");
        var stdValRenderings = ContextExtensions.UtfService.GetItemFieldValue(standardValuesPath, "__Renderings");

        //find UID of control in base rendering 
        XDocument stdRenderingField = XDocument.Parse(stdValRenderings);
        var uid =
          stdRenderingField.Descendants(XName.Get("r"))
            .First(r => r.Attribute(XName.Get("id"))?.Value == property.ControlId)
            .Attribute(XName.Get("uid"))
            .Value;
        var targetXml = renderingFieldXml.Descendants(XName.Get("r")).First(r => r.Attribute(XName.Get("uid"))?.Value == uid);

        var value = property.Value;
        var localName = "ds";

        switch (property.Type)
        {
          case "SearchResultLimit":
            value = "SearchResultLimit=" + property.Value;
            localName = "par";
            break;
        }

        targetXml.SetAttributeValue(XName.Get(localName, "s"), value);
        EditItem(property.ItemPath, "__Renderings", renderingFieldXml.ToString());

      }

    }


    [Given(@"New item version was added")]
    public void GivenNewItemVersionWasAdded(Table table)
    {
      var versionInfos = table.Rows.Select(x => new { IdOrPath = x[0], Language = x[1] });

      foreach (var versionInfo in versionInfos)
      {
        ContextExtensions.HelperService.AddItemVersion(versionInfo.IdOrPath, versionInfo.Language);
      }
    }
    [AfterFeature]
    public static void TeardownTest()
    {
      try
      {
        FeatureContext.Current.Get<IWebDriver>().Quit();
      }
      catch (Exception)
      {
        // Ignore errors if unable to close the browser
      }
    }
  }
}
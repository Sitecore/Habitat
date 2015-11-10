namespace Habitat.Accounts.Tests
{
  using System;
  using FluentAssertions;
  using Habitat.Accounts.Services;
  using Habitat.Accounts.Tests.Extensions;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using NSubstitute;
  using Sitecore;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Exceptions;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Sites;
  using Xunit;

  public class AccountSettingsServiceTests
  {
    [Theory]
    [AutoDbData]
    public void GetForgotPasswordMailTemplateShouldRetreiveSourceAddressFromItem(Db db)
    {
      var fakeSite = BuildUpSiteContext(db);
      using (new SiteContextSwitcher(fakeSite))
      {
        var settings = new AccountsSettingsService();
        var mailTemplate = settings.GetForgotPasswordMailTemplate();
        mailTemplate.From.Address.Should().Be("from@sc.net");
      }
    }

    [Theory]
    [AutoDbData]
    public void GetForgotPasswordMailTemplateShouldRetreiveBodyFromItem(Db db)
    {
      var fakeSite = BuildUpSiteContext(db);
      using (new SiteContextSwitcher(fakeSite))
      {
        var settings = new AccountsSettingsService();
        var mailTemplate = settings.GetForgotPasswordMailTemplate();
        mailTemplate.Body.Should().Be("restore password $pass$ body");
      }
    }

    [Theory]
    [AutoDbData]
    public void GetForgotPasswordMailTemplateShouldRetreiveSubjectFromItem(Db db)
    {
      var fakeSite = BuildUpSiteContext(db);
      using (new SiteContextSwitcher(fakeSite))
      {
        var settings = new AccountsSettingsService();
        var mailTemplate = settings.GetForgotPasswordMailTemplate();
        mailTemplate.Subject.Should().Be("restore password subject");
      }
    }


    [Theory]
    [AutoDbData]
    public void GetForgotPasswordMailTemplateShouldThrowExceptionWhenFromAddressIsEmpty(Db db)
    {
      var fakeSite = BuildUpSiteContext(db, from: null);
      using (new SiteContextSwitcher(fakeSite))
      {
        var settings = new AccountsSettingsService();
        Action act = () => settings.GetForgotPasswordMailTemplate();
        act.ShouldThrow<InvalidValueException>().WithMessage("'From' field in mail template should be set");
      }
    }


    [Theory]
    [AutoDbData]
    public void GetForgotPasswordMailTemplateShouldNotThrowExceptionWhenSubjIsEmpty(Db db)
    {
      var fakeSite = BuildUpSiteContext(db, subj: null);
      using (new SiteContextSwitcher(fakeSite))
      {
        var settings = new AccountsSettingsService();
        settings.GetForgotPasswordMailTemplate().Subject.Should().BeEmpty();
      }
    }

    [Theory]
    [AutoDbData]
    public void GetForgotPasswordMailTemplateShouldThrowExceptionWhenMailTemplateNotfound(Db db)
    {
      var fakeSite = BuildUpSiteContext(db, from: null);
      db.GetItem("/sitecore/content/siteroot").DeleteChildren();
      using (new SiteContextSwitcher(fakeSite))
      {
        var settings = new AccountsSettingsService();
        Action act = () => settings.GetForgotPasswordMailTemplate();
        act.ShouldThrow<ItemNotFoundException>();
      }
    }

    [Theory]
    [AutoDbData]
    public void GetForgotPasswordMailTemplateShouldNotThrowExceptionWhenBodyIsEmpty(Db db)
    {
      var fakeSite = BuildUpSiteContext(db, null);
      
      using (new SiteContextSwitcher(fakeSite))
      {
        var settings = new AccountsSettingsService();
        settings.GetForgotPasswordMailTemplate().Body.Should().BeEmpty();
      }
    }


    private static SiteContext BuildUpSiteContext(Db db, string body = "restore password $pass$ body", string from = "from@sc.net", string subj = "restore password subject")
    {
      var template = ID.NewID;
      db.Add(new DbItem("siteroot")
      {
        TemplateID = Templates.AccountsSettings.ID,
        Fields =
        {
          new DbField("FogotPasswordMailTemplate", Templates.AccountsSettings.Fields.FogotPasswordMailTemplate)
          {
            Value = template.ToString()
          }
        },
        Children =
        {
          new DbItem("mailtemplate", template, Templates.MailTemplate.ID)
          {
            {
              Templates.MailTemplate.Fields.Body, body
            },
            {
              Templates.MailTemplate.Fields.From, from
            },
            {
              Templates.MailTemplate.Fields.Subject, subj
            }
          }
        }
      });

      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore/content/siteroot"
        },
        {
          "database", "master"
        }
      }) as SiteContext;
      Context.Item = db.GetItem(template);
      return fakeSite;
    }

    [Theory, AutoDbData]
    public void GetPageLinkOrDefaultShouldThrowIfDefaultIsNull(Item item, ID id, AccountsSettingsService accountSettingsService)
    {
      //var accountSettingsService = Substitute.ForPartsOf<AccountsSettingsService>();
      //accountSettingsService
      accountSettingsService.Invoking(x => x.GetPageLinkOrDefault(item, id, null)).ShouldThrow<ArgumentNullException>();
    }

    [Theory, AutoDbData]
    public void GetPageLinkOrDefaultShouldReturnDefault(Item item,ID id, Item defaultItem)
    {
      var accountSettingsService = Substitute.ForPartsOf<AccountsSettingsService>();
      accountSettingsService.When(x => x.GetPageLink(item, id)).DoNotCallBase();
      accountSettingsService.GetPageLink(Arg.Any<Item>(), Arg.Any<ID>()).Returns(x => { throw new Exception(); });

      var result = accountSettingsService.GetPageLinkOrDefault(item, id, defaultItem);

      result.Should().Be(defaultItem.Url());
    }

    [Theory, AutoDbData]
    public void GetPageLinkOrDefaultShouldReturnGetPageLinkResult(Item item, ID id, Item defaultItem, string returnUrl)
    {
      var accountSettingsService = Substitute.ForPartsOf<AccountsSettingsService>();
      accountSettingsService.When(x => x.GetPageLink(item, id)).DoNotCallBase();
      accountSettingsService.GetPageLink(Arg.Any<Item>(), Arg.Any<ID>()).Returns(x => returnUrl);

      var result = accountSettingsService.GetPageLinkOrDefault(item, id, defaultItem);

      result.Should().Be(returnUrl);
    }
  }
}
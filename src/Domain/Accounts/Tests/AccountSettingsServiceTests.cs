namespace Habitat.Accounts.Tests
{
  using System;
  using FluentAssertions;
  using Habitat.Accounts.Services;
  using Habitat.Accounts.Tests.Extensions;
  using Sitecore;
  using Sitecore.Collections;
  using Sitecore.Data;
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
            {
              "en", template.ToString()
            }
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

      Context.Item = db.GetItem(template);

      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore/content/siteroot"
        },
        {
          "database", "master"
        }
      }) as SiteContext;
      return fakeSite;
    }
  }
}
namespace Sitecore.Feature.Demo.Tests.Models
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Model;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class ContactInformationTests
  {
    [Theory]
    [AutoDbData]
    public void NoOfVisits_ShouldReturnFromProvider(IContactProfileProvider provider, int number)
    {
      provider.Contact.System.VisitCount.Returns(number);
      var model = new ContactInformation(provider);
      model.NoOfVisits.Should().Be(number);
    }

    [Theory]
    [AutoDbData]
    public void EngagementValue_ShouldReturnFromProvider(IContactProfileProvider provider, int number)
    {
      provider.Contact.System.Value.Returns(number);
      var model = new ContactInformation(provider);
      model.EngagementValue.Should().Be(number);
    }

    [Theory]
    [AutoDbData]
    public void ID_ShouldReturnFromProvider(IContactProfileProvider provider, Guid expectedId)
    {
      provider.Contact.ContactId.Returns(expectedId);
      var model = new ContactInformation(provider);
      model.Id.Should().Be(expectedId);
    }

    [Theory]
    [AutoDbData]
    public void Identifier_ShouldReturnFromProvider(IContactProfileProvider provider, string expectedId)
    {
      provider.Contact.Identifiers.Identifier.Returns(expectedId);
      var model = new ContactInformation(provider);
      model.Identifier.Should().Be(expectedId);
    }


    [Theory]
    [AutoDbData]
    public void IdentificationStatus_ShouldReturnFromProvider(IContactProfileProvider provider, ContactIdentificationLevel expected)
    {
      provider.Contact.Identifiers.IdentificationLevel.Returns(expected);
      var model = new ContactInformation(provider);
      model.IdentificationStatus.Should().Be(expected.ToString());
    }

  }
}
namespace Sitecore.Feature.Accounts.Tests.Services.FacetUpdaters
{
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Feature.Accounts.Services.FacetUpdaters;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Security;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Operations;
    using Xunit;

    public class EmailFacetUpdaterTests
    {
        [Theory]
        [AutoDbData]
        public void SetFacets_NoEmailList_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string email)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile.Email.Returns(email);
            var facetUpdater = new EmailFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeTrue();
            xdbContext.Received(1).RegisterOperation(Arg.Is<SetFacetOperation>(x =>
                x.FacetReference.FacetKey == EmailAddressList.DefaultFacetKey &&
                ((EmailAddressList)x.Facet).PreferredEmail.SmtpAddress == email));
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_DifferentPreferredEmail_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string email, string differentEmail)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile.Email.Returns(email);
            Sitecore.XConnect.Serialization.DeserializationHelpers.SetFacet(xdbContact, EmailAddressList.DefaultFacetKey, new EmailAddressList(new EmailAddress(differentEmail, false), null));
            var facetUpdater = new EmailFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeTrue();
            xdbContext.Received(1).RegisterOperation(Arg.Is<SetFacetOperation>(x =>
                x.FacetReference.FacetKey == EmailAddressList.DefaultFacetKey &&
                ((EmailAddressList)x.Facet).PreferredEmail.SmtpAddress == email));
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoProfileEMail_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            var facetUpdater = new EmailFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeFalse();
            xdbContext.DidNotReceiveWithAnyArgs().RegisterOperation(Arg.Any<IXdbOperation>());
        }

        public void SetFacets_NoChanges_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string email)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile.Email.Returns(email);
            Sitecore.XConnect.Serialization.DeserializationHelpers.SetFacet(xdbContact, EmailAddressList.DefaultFacetKey, new EmailAddressList(new EmailAddress(email, false), null));
            var facetUpdater = new EmailFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeFalse();
            xdbContext.DidNotReceiveWithAnyArgs().RegisterOperation(Arg.Any<IXdbOperation>());
        }
    }
}

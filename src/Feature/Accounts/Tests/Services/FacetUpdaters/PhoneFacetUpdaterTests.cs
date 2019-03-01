namespace Sitecore.Feature.Accounts.Tests.Services.FacetUpdaters
{
    using FluentAssertions;
    using NSubstitute;
    using Sitecore.Feature.Accounts.Services.FacetUpdaters;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Operations;
    using Xunit;

    public class PhoneFacetUpdaterTests
    {
        [Theory]
        [AutoDbData]
        public void SetFacets_NoPhoneList_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string phone)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[Accounts.Constants.UserProfile.Fields.PhoneNumber] = phone;
            var facetUpdater = new PhoneFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeTrue();
            xdbContext.Received(1).RegisterOperation(Arg.Is<SetFacetOperation>(x =>
                x.FacetReference.FacetKey == PhoneNumberList.DefaultFacetKey &&
                ((PhoneNumberList)x.Facet).PreferredPhoneNumber.Number == phone));
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_DifferentPhone_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string phone, string differentPhone)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[Accounts.Constants.UserProfile.Fields.PhoneNumber] = phone;
            Sitecore.XConnect.Serialization.DeserializationHelpers.SetFacet(xdbContact, PhoneNumberList.DefaultFacetKey, new PhoneNumberList(new PhoneNumber(null, differentPhone), null));
            var facetUpdater = new PhoneFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeTrue();
            xdbContext.Received(1).RegisterOperation(Arg.Is<SetFacetOperation>(x =>
                x.FacetReference.FacetKey == PhoneNumberList.DefaultFacetKey &&
                ((PhoneNumberList)x.Facet).PreferredPhoneNumber.Number == phone));
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoProfilePhone_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            var facetUpdater = new PhoneFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeFalse();
            xdbContext.DidNotReceiveWithAnyArgs().RegisterOperation(Arg.Any<IXdbOperation>());
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoChanges_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string phone)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[Accounts.Constants.UserProfile.Fields.PhoneNumber] = phone;
            Sitecore.XConnect.Serialization.DeserializationHelpers.SetFacet(xdbContact, PhoneNumberList.DefaultFacetKey, new PhoneNumberList(new PhoneNumber(null, phone), null));
            var facetUpdater = new PhoneFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeFalse();
            xdbContext.DidNotReceiveWithAnyArgs().RegisterOperation(Arg.Any<IXdbOperation>());
        }
    }
}

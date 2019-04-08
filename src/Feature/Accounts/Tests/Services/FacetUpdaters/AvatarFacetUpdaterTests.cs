namespace Sitecore.Feature.Accounts.Tests.Services.FacetUpdaters
{
    using FluentAssertions;
    using NSubstitute;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Feature.Accounts.Services.FacetUpdaters;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Operations;
    using Xunit;

    public class AvatarFacetUpdaterTests
    {
        [Theory]
        [AutoDbData]
        public void SetFacets_NoAvatar_ShouldSetFacet(IWebClient webClient, Contact xdbContact, IXdbContext xdbContext, string url, string mimeType, byte[] picture)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[Accounts.Constants.UserProfile.Fields.PictureUrl] = url;
            userProfile[Accounts.Constants.UserProfile.Fields.PictureMimeType] = mimeType;
            webClient.DownloadData(url).Returns(picture);
            var facetUpdater = new AvatarFacetUpdater(webClient);

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeTrue();
            xdbContext.Received(1).RegisterOperation(Arg.Is<SetFacetOperation>(x =>
                x.FacetReference.FacetKey == Avatar.DefaultFacetKey &&
                ((Avatar)x.Facet).Picture == picture &&
                ((Avatar)x.Facet).MimeType == mimeType));
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_DifferentAvatar_ShouldSetFacet(IWebClient webClient, Contact xdbContact, IXdbContext xdbContext, string url, string mimeType, byte[] picture, byte[] differentPicture)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[Accounts.Constants.UserProfile.Fields.PictureUrl] = url;
            userProfile[Accounts.Constants.UserProfile.Fields.PictureMimeType] = mimeType;
            Sitecore.XConnect.Serialization.DeserializationHelpers.SetFacet(xdbContact, Avatar.DefaultFacetKey, new Avatar(mimeType, differentPicture));
            webClient.DownloadData(url).Returns(picture);
            var facetUpdater = new AvatarFacetUpdater(webClient);

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeTrue();
            xdbContext.Received(1).RegisterOperation(Arg.Is<SetFacetOperation>(x =>
                x.FacetReference.FacetKey == Avatar.DefaultFacetKey &&
                ((Avatar)x.Facet).Picture == picture &&
                ((Avatar)x.Facet).MimeType == mimeType));
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoProfileUrl_ShouldReturnFalse(IWebClient webClient, Contact xdbContact, IXdbContext xdbContext)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            var facetUpdater = new AvatarFacetUpdater(webClient);

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeFalse();
            xdbContext.DidNotReceiveWithAnyArgs().RegisterOperation(Arg.Any<IXdbOperation>());
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_SameAvatar_ShouldReturnFalse(IWebClient webClient, Contact xdbContact, IXdbContext xdbContext, string url, string mimeType, byte[] picture)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[Accounts.Constants.UserProfile.Fields.PictureUrl] = url;
            userProfile[Accounts.Constants.UserProfile.Fields.PictureMimeType] = mimeType;
            Sitecore.XConnect.Serialization.DeserializationHelpers.SetFacet(xdbContact, Avatar.DefaultFacetKey, new Avatar(mimeType, picture));
            webClient.DownloadData(url).Returns(picture);
            var facetUpdater = new AvatarFacetUpdater(webClient);

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeFalse();
            xdbContext.DidNotReceiveWithAnyArgs().RegisterOperation(Arg.Any<IXdbOperation>());
        }
    }
}

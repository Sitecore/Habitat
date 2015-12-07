namespace Sitecore.Feature.Accounts.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Xunit;

  public class ContactProfileProviderTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldReturnContact([NoAutoProperties] ContactProfileProvider provider, ITracker tracker, Contact contact)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      using (new TrackerSwitcher(tracker))
      {
        tracker.Contact.Should().NotBeNull();
        provider.Contact.ShouldBeEquivalentTo(tracker.Contact);
      }
    }
  }
}
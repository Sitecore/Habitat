using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat.Accounts.Tests
{
  using FluentAssertions;
  using Habitat.Accounts.Services;
  using Habitat.Accounts.Tests.Extensions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
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
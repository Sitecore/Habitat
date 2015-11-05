using FluentAssertions;
using Habitat.Framework.SitecoreExtensions.Repositories;
using Habitat.Framework.SitecoreExtensions.Tests.Common;
using Xunit;

namespace Habitat.Framework.SitecoreExtensions.Tests.Repositories
{
  public class DatabaseRepositoryTests
  {
    [Theory, AutoDbData]
    public void ShouldReturnMasterDB()
    {
      DatabaseRepository.GetActiveDatabase().Name.Should().Be("master");
    }

  }
}

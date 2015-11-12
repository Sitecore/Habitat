namespace Habitat.Framework.SitecoreExtensions.Tests.Repositories
{
  using FluentAssertions;
  using Habitat.Framework.SitecoreExtensions.Repositories;
  using Habitat.Framework.SitecoreExtensions.Tests.Common;
  using Xunit;

  public class DatabaseRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldReturnMasterDB()
    {
      DatabaseRepository.GetActiveDatabase().Name.Should().Be("master");
    }
  }
}
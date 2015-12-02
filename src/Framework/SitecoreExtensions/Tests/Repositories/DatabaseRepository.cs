namespace Sitecore.Framework.SitecoreExtensions.Tests.Repositories
{
  using FluentAssertions;
  using Sitecore.Framework.SitecoreExtensions.Repositories;
  using Sitecore.Framework.SitecoreExtensions.Tests.Common;
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
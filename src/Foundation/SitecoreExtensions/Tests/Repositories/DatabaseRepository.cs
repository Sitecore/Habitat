namespace Sitecore.Foundation.SitecoreExtensions.Tests.Repositories
{
  using FluentAssertions;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
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
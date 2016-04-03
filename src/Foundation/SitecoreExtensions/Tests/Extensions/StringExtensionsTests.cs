namespace Sitecore.Foundation.SitecoreExtensions.Tests.Extensions
{
  using FluentAssertions;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Xunit;

  public class StringExtensionsTests
  {
    [Theory]
    [InlineData("TestString", "Test String")]
    [InlineData("Test String", "Test String")]
    public void Humanize_ShouldReturnValueSplittedWithWhitespaces(string input, string expected)
    {
      input.Humanize().Should().Be(expected);
    }
  }
}
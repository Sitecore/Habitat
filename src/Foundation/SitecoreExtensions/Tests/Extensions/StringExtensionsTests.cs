namespace Sitecore.Foundation.SitecoreExtensions.Tests.Extensions
{
  using System;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Tests.Common;
  using Xunit;

  public class StringExtensionsTests
  {
    [Theory]
    [InlineData("TestString","Test String")]
    [InlineData("Test String","Test String")]
    public void Humanize_ShouldReturnValueSplittedWithWhitespaces(string input, string expected)
    {
      input.Humanize().Should().Be(expected);
    }
  }
}
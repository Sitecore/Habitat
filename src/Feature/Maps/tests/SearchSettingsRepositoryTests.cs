namespace Sitecore.Feature.Maps.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Mvc;
  using Controllers;
  using FakeDb;
  using FluentAssertions;
  using Repositories;
  using Xunit;
  using Foundation.Testing.Attributes;
  using Models;
  using NSubstitute;
  using Pipelines;

  public class SearchSettingsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_ShouldReturnSettingWithSingleTemplate()
    {
      new SearchSettingsRepository().Get().Templates.Single().Guid.Should().Be(Templates.MapPoint.ID.Guid);
    }
  }
}
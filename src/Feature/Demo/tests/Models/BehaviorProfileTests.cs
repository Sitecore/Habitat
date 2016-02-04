namespace Sitecore.Feature.Demo.Tests.Models
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class BehaviorProfileTests
  {
    [Theory]
    [AutoDbData]
    public void Id_ShouldReturnFromContext(IBehaviorProfileContext ctx)
    {
      ctx.Id.Returns(ID.NewID);
      var model = new BehaviorProfile(ctx);
      model.Id.Should().Be(ctx.Id.Guid);

    }
    [Theory]
    [AutoDbData]
    public void NumberOfTimesScored_ShouldReturnFromContext(IBehaviorProfileContext ctx, int count)
    {
      ctx.NumberOfTimesScored.Returns(count);
      var model = new BehaviorProfile(ctx);
      model.NumberOfTimesScored.Should().Be(count);

    }


    [Theory]
    [AutoDbData]
    public void Scores_ShouldReturnFromContext(IBehaviorProfileContext ctx, float value, [Content] Item ctxItem)
    {
      var score = new KeyValuePair<ID, float>(ctxItem.ID, value);
      ctx.Scores.Returns(new [] { score });
      var model = new BehaviorProfile(ctx);
      model.Scores.Count().Should().Be(1);
      model.Scores.First().Id.Should().Be(score.Key.Guid);
      model.Scores.First().Value.Should().Be(score.Value);
      model.Scores.First().Name.Should().Be(ctxItem.DisplayName);
    }


    [Theory]
    [AutoDbData]
    public void Name_ShouldReturnDisplayNameOfItem(IBehaviorProfileContext ctx, [Content] Item ctxItem)
    {
      ctx.Id.Returns(ctxItem.ID);
      var model = new BehaviorProfile(ctx);
      model.Name.Should().Be(ctxItem.DisplayName);

    }

  }
}
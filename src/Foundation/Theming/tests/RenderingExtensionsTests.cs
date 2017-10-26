namespace Sitecore.Foundation.Theming.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Foundation.Theming.Extensions;
  using Sitecore.Foundation.Theming.Tests.Extensions;
  using Sitecore.Mvc.Presentation;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.SecurityModel;

  public class RenderingExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void IsFixedHeight_PropertySetInRendering_ShouldReturnTrue([Substitute]Rendering rendering)
    {
      var parameters = new RenderingParameters($"{Constants.IsFixedHeightLayoutParameters.FixedHeight}=1");
      rendering.Parameters.Returns(parameters);
      rendering.IsFixedHeight().Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void IsFixedHeight_PropertyIsNotSet_ShouldReturnTrue([Substitute]Rendering rendering)
    {
      var parameters = new RenderingParameters("");
      rendering.Parameters.Returns(parameters);
      rendering.IsFixedHeight().Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsFixedHeight_PropertySetToZero_ShouldReturnTrue([Substitute]Rendering rendering)
    {
      var parameters = new RenderingParameters($"{Constants.IsFixedHeightLayoutParameters.FixedHeight}=0");
      rendering.Parameters.Returns(parameters);
      rendering.IsFixedHeight().Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void GetBackgroundClass_ClassFieldIsNotSet_ShouldReturnEmptyString(Db db,[Substitute] Rendering rendering, ID itemId, [Content]DbItem item, [Content] DbItem renderingItem)
    {
      var rItem = db.GetItem(renderingItem.ID);
      rendering.RenderingItem.Returns(rItem);
      var parameters = new RenderingParameters($"{Constants.BackgroundLayoutParameters.Background}={item.ID}");
      rendering.Parameters.Returns(parameters);
      var bgClass = rendering.GetBackgroundClass();
      bgClass.Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void GetBackgroundClass_ClassFieldIsSet_ShouldReturnClassValue(Db db, [Substitute] Rendering rendering, ID itemId, string itemName,[Content] DbItem renderingItem, string classValue)
    {
      var rItem = db.GetItem(renderingItem.ID);
      db.Add(new DbItem(itemName, itemId) {new DbField(Templates.Style.Fields.Class) { {"en", classValue}} });
      var backgroundClassItem = db.GetItem(itemId);
       
      rendering.RenderingItem.Returns(rItem);
      var parameters = new RenderingParameters($"{Constants.BackgroundLayoutParameters.Background}={backgroundClassItem.ID}");
      rendering.Parameters.Returns(parameters);
      var bgClass = rendering.GetBackgroundClass();
      bgClass.Should().BeEquivalentTo(classValue);
    }

    [Theory]
    [AutoDbData]
    public void GetBackgroundClass_BackgroundItemDoesNotExists_ShouldReturnEmptyString(Db db, [Substitute] Rendering rendering, ID itemId, [Content] DbItem renderingItem)
    {
      var rItem = db.GetItem(renderingItem.ID);

      rendering.RenderingItem.Returns(rItem);
      var parameters = new RenderingParameters($"{Constants.BackgroundLayoutParameters.Background}={itemId}");
      rendering.Parameters.Returns(parameters);
      var bgClass = rendering.GetBackgroundClass();
      bgClass.Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void GetBackgroundClass_BackgroundParameterIsNotSet_ShouldReturnEmptyString(Db db, [Substitute] Rendering rendering, ID itemId, [Content] DbItem renderingItem)
    {
      var rItem = db.GetItem(renderingItem.ID);
      rendering.RenderingItem.Returns(rItem);
      var parameters = new RenderingParameters("");
      rendering.Parameters.Returns(parameters);
      var bgClass = rendering.GetBackgroundClass();
      bgClass.Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void IsContainerFluid_ParameterIsSet_ShouldReturnTrue([Substitute] Rendering rendering)
    {
      var parameters = new RenderingParameters($"{Constants.HasContainerLayoutParameters.IsFluid}=1");
      rendering.Parameters.Returns(parameters);
      var isFluid = rendering.IsContainerFluid();
      isFluid.Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void IsContainerFluid_ParameterIsSetToZero_ShouldReturnFalse([Substitute] Rendering rendering)
    {
      var parameters = new RenderingParameters($"{Constants.HasContainerLayoutParameters.IsFluid}=0");
      rendering.Parameters.Returns(parameters);
      var isFluid = rendering.IsContainerFluid();
      isFluid.Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsContainerFluid_ParameterIsNotSet_ShouldReturnFalse([Substitute] Rendering rendering)
    {
      var parameters = new RenderingParameters("");
      rendering.Parameters.Returns(parameters);
      var isFluid = rendering.IsContainerFluid();
      isFluid.Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void GetContainerClass_IsFluidSetToTrue_ShouldReturnContainerFluid([Substitute] Rendering rendering)
    {
      var parameters = new RenderingParameters($"{Constants.HasContainerLayoutParameters.IsFluid}=1");
      rendering.Parameters.Returns(parameters);
      var containerClass = rendering.GetContainerClass();
      containerClass.ShouldBeEquivalentTo("container-fluid");
    }

    [Theory]
    [AutoDbData]
    public void GetContainerClass_IsFluidSetToFalse_ShouldReturnContainer([Substitute] Rendering rendering)
    {
      var parameters = new RenderingParameters($"{Constants.HasContainerLayoutParameters.IsFluid}=0");
      rendering.Parameters.Returns(parameters);
      var containerClass = rendering.GetContainerClass();
      containerClass.ShouldBeEquivalentTo("container");
    }


  }
}

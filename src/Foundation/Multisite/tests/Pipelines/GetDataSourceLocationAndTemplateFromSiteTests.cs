namespace Sitecore.Foundation.Multisite.Tests.Pipelines
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Foundation.Multisite.Pipelines;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.Multisite.Tests.Extensions;
  using Sitecore.Pipelines.GetRenderingDatasource;
  using Xunit;

  public class GetDatasourceLocationAndTemplateFromSiteTests
  {
    [Theory]
    [AutoDbData]
    public void Process_DatasourceProvidersAreNull_SourcesAndTemplateAreNotSet([Frozen]RenderingDatasourceProvider provider, GetDatasourceLocationAndTemplateFromSite processor, DbItem renderingItem, Db db, string settingName)
    {
      var setting = settingName.Replace("-", string.Empty);
      renderingItem.Add(new DbField("Datasource Location") { {"en", $"site:{setting}"} });
      db.Add(renderingItem);
      var rendering = db.GetItem(renderingItem.ID);
      var args = new GetRenderingDatasourceArgs(rendering);
      processor.Process(args);
      args.DatasourceRoots.Count.Should().Be(0);
      args.Prototype.Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void Process_DatasourceProviderIsNotNull_SourcesAndTemplateAreSet(IRenderingDatasourceProvider provider, DbItem renderingItem, Db db, string settingName, Item[] sources, Item sourceTemplate)
    {
      provider.GetDatasourceLocations(Arg.Any<Item>(), Arg.Any<string>()).Returns(sources);
      provider.GetDatasourceTemplate(Arg.Any<Item>(), Arg.Any<string>()).Returns(sourceTemplate);

      var setting = settingName.Replace("-", string.Empty);
      renderingItem.Add(new DbField(Templates.RenderingOptions.Fields.DatasourceLocation) { { "en", $"site:{setting}" } });

      db.Add(renderingItem);
      var rendering = db.GetItem(renderingItem.ID);

      var processor = new GetDatasourceLocationAndTemplateFromSite(provider);
      var args = new GetRenderingDatasourceArgs(rendering);
      processor.Process(args);
      args.DatasourceRoots.Should().Contain(sources);
      args.Prototype.Should().Be(sourceTemplate);
    }

    [Theory]
    [AutoDbData]
    public void Process_SiteSettingIsNotSet_SourcesAndTemplateAreNotSet(GetDatasourceLocationAndTemplateFromSite processor, Item renderingItem)
    {
      var args = new GetRenderingDatasourceArgs(renderingItem);
      processor.Process(args);
      args.DatasourceRoots.Count.Should().Be(0);
      args.Prototype.Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void Process_SiteSettingNameHasWrongFirmat_SourcesAndTemplateAreNotSet(GetDatasourceLocationAndTemplateFromSite processor, DbItem renderingItem, Db db, string settingName)
    {
      renderingItem.Add(new DbField("Datasource Location") { { "en", $"site:{settingName}" } });
      db.Add(renderingItem);
      var rendering = db.GetItem(renderingItem.ID);
      var args = new GetRenderingDatasourceArgs(rendering);
      processor.Process(args);
      args.DatasourceRoots.Count.Should().Be(0);
      args.Prototype.Should().BeNull();
    }
  }
}

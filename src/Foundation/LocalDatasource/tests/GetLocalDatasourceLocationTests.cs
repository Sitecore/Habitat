namespace Sitecore.Foundation.LocalDatasource.Tests
{
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.LocalDatasource.Infrastructure.Pipelines;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Pipelines.GetRenderingDatasource;
  using Xunit;

  public class GetLocalDatasourceLocationTests
  {
    [Theory]
    [AutoDbData]
    public void Process_LocalDatasourceExists_ShouldResolveDatasourceRoot(GetLocalDatasourceLocation processor, Db db, [Content] Item contextItem)
    {
      //arrange
      db.Add(new DbItem("rendering")
      {
        {
          Templates.RenderingOptions.Fields.SupportsLocalDatasource, "1"
        }
      });

      var datasourceFolder = contextItem.Add("_Local", contextItem.Template);

      var renderingItem = db.GetItem("/sitecore/content/rendering");
      var getRenderingDatasourceArgs = new GetRenderingDatasourceArgs(renderingItem)
      {
        ContextItemPath = contextItem.Paths.FullPath
      };
      //act
      processor.Process(getRenderingDatasourceArgs);
      //assert
      getRenderingDatasourceArgs.DatasourceRoots.First().ID.Should().Be(datasourceFolder.ID);
    }

    [Theory]
    [AutoDbData]
    public void Process_LocalDatasourceNotExist_ShouldCreateDatasourceRoot(GetLocalDatasourceLocation processor, Db db, [Content] Item contextItem, [Content] DbTemplate template)
    {
      //arrange
      db.Add(new DbItem("rendering")
      {
        {
          Templates.RenderingOptions.Fields.SupportsLocalDatasource, "1"
        }
      });

      var renderingItem = db.GetItem("/sitecore/content/rendering");
      var getRenderingDatasourceArgs = new GetRenderingDatasourceArgs(renderingItem)
      {
        ContextItemPath = contextItem.Paths.FullPath
      };

      //act
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", template.ID.ToString()))
      {
        processor.Process(getRenderingDatasourceArgs);
      }
      //assert
      var datasourceFolder = contextItem.GetChildren().First();
      getRenderingDatasourceArgs.DatasourceRoots.First().ID.Should().Be(datasourceFolder.ID);
      datasourceFolder.TemplateID.Should().Be(template.ID);
    }


    [Theory]
    [AutoDbData]
    public void Process_ContextItemNotSet_ShouldReturnEmptyRoots(GetLocalDatasourceLocation processor, Db db, [Content] Item contextItem, [Content] DbTemplate template)
    {
      //arrange
      db.Add(new DbItem("rendering")
      {
        {
          Templates.RenderingOptions.Fields.SupportsLocalDatasource, "1"
        }
      });

      var renderingItem = db.GetItem("/sitecore/content/rendering");
      var getRenderingDatasourceArgs = new GetRenderingDatasourceArgs(renderingItem)
      {
        ContextItemPath = ID.NewID.ToString()
      };

      //act
      processor.Process(getRenderingDatasourceArgs);
      //assert
      getRenderingDatasourceArgs.DatasourceRoots.Should().BeEmpty();
    }


    [Theory]
    [AutoDbData]
    public void Process_SupportsLocalDatasourceFieldNotSet_ShouldReturnEmptyRoots(GetLocalDatasourceLocation processor, Db db, [Content] Item renderingItem)
    {
      var getRenderingDatasourceArgs = new GetRenderingDatasourceArgs(renderingItem);

      //act
      processor.Process(getRenderingDatasourceArgs);
      //assert
      getRenderingDatasourceArgs.DatasourceRoots.Should().BeEmpty();
    }


    [Theory]
    [AutoDbData]
    public void Process_DatasourceTemplateNotSet_ShouldReturnEmptyRoots(GetLocalDatasourceLocation processor, Db db, [Content] Item contextItem)
    {
      //arrange
      db.Add(new DbItem("rendering")
      {
        {
          Templates.RenderingOptions.Fields.SupportsLocalDatasource, "1"
        }
      });

      var renderingItem = db.GetItem("/sitecore/content/rendering");
      var getRenderingDatasourceArgs = new GetRenderingDatasourceArgs(renderingItem)
      {
        ContextItemPath = contextItem.Paths.FullPath
      };

      //act
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", ID.NewID.ToString()))
      {
        processor.Process(getRenderingDatasourceArgs);
      }
      //assert
      getRenderingDatasourceArgs.DatasourceRoots.Should().BeEmpty();
    }
  }
}
using FluentAssertions;
using NSubstitute;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Sitecore.Foundation.Multisite.Pipelines;
using Sitecore.Foundation.Multisite.Providers;
using Sitecore.Foundation.Multisite.Tests.Extensions;
using Sitecore.Pipelines.GetRenderingDatasource;
using Xunit;
using Sitecore.Pipelines.GetLookupSourceItems;

namespace Sitecore.Foundation.Multisite.Tests.Pipelines
{
    using NSubstitute.ReturnsExtensions;

    public class ProcessSiteSourceTests
    {
        [Theory]
        [AutoDbData]
        public void Process_DatasourceIsNotASiteDatasource(ProcessSiteSource processor)
        {
            var dataSource = "\\sitecore\\not\\site\\datasource";
            var args = new GetLookupSourceItemsArgs
                           {
                               Source = dataSource
                           };
            processor.Process(args);
            args.Source.Should().Be(dataSource);
        }

        [Theory]
        [AutoDbData]
        public void Process_DatasourceIsEmpty(ProcessSiteSource processor)
        {
            string dataSource = string.Empty;
            var args = new GetLookupSourceItemsArgs
                           {
                               Source = dataSource
                           };
            processor.Process(args);
            args.Source.Should().Be(dataSource);
        }


        [Theory]
        [AutoDbData]
        public void Process_DatasourceIsSiteDatasource_NotUnderASite(IFieldDatasourceProvider provider, string settingName)
        {
            provider.GetDatasourceLocation(Arg.Any<Item>(), Arg.Any<string>()).Returns(new Item[] { });
            var setting = settingName.Replace("-", string.Empty);
            var dataSource = $"site:{setting}";
            var args = new GetLookupSourceItemsArgs
                           {
                               Source = dataSource
                           };

            var processor = new ProcessSiteSource(provider);
            processor.Process(args);
            args.Source.Should().Be(string.Empty);
        }

        [Theory]
        [AutoDbData]
        public void Process_DatasourceIsSiteDatasource_UnderASite(IFieldDatasourceProvider provider, string settingName, Item item)
        {
            provider.GetDatasourceLocation(Arg.Any<Item>(), Arg.Any<string>()).Returns(new Item[] { item });
            var setting = settingName.Replace("-", string.Empty);
            var dataSource = $"site:{setting}";
            var args = new GetLookupSourceItemsArgs
                           {
                               Source = dataSource
                           };

            var processor = new ProcessSiteSource(provider);
            processor.Process(args);
            args.Source.Should().Be(item.Paths.FullPath);
        }

        [Theory]
        [AutoDbData]
        public void Process_DatasourceIsSiteDatasource_UnderASite_IsATreeListField(IFieldDatasourceProvider provider, string settingName, Item item)
        {
            provider.GetDatasourceLocation(Arg.Any<Item>(), Arg.Any<string>()).Returns(new Item[] { item });
            var setting = settingName.Replace("-", string.Empty);
            var dataSource = $"query:site:{setting}";
            var args = new GetLookupSourceItemsArgs
                           {
                               Source = dataSource
                           };

            var processor = new ProcessSiteSource(provider);
            processor.Process(args);
            args.Source.Should().Be(item.Paths.FullPath);
        }
    }
}

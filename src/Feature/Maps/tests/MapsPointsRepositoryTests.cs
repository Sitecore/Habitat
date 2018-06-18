namespace Sitecore.Feature.Maps.Tests
{
    using System;
    using System.Linq;
    using Data;
    using FakeDb;
    using FluentAssertions;
    using Ploeh.AutoFixture.Xunit2;
    using Repositories;
    using Xunit;

    public class MapsPointsRepositoryTests
    {
        [Theory]
        [Foundation.Testing.Attributes.AutoDbData]
        public void GetAll_NullPassed_ShouldThrowArgumentNullException([Frozen] Foundation.Indexing.Repositories.ISearchServiceRepository searchServiceRepository)
        {
            var repository = new MapPointRepository(searchServiceRepository);
            Action a = () => repository.GetAll(null);
            a.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [Foundation.Testing.Attributes.AutoDbData]
        public void GetAll_PointItemPassed_ShouldReturnSinglePoint(Db db, [Frozen] Foundation.Indexing.Repositories.ISearchServiceRepository searchServiceRepository)
        {
            var itemid = ID.NewID;
            db.Add(new DbItem("point", itemid, Templates.MapPoint.ID)
            {
                {Templates.MapPoint.Fields.Name, "nameField"}
            });
            var repository = new MapPointRepository(searchServiceRepository);
            var actual = repository.GetAll(db.GetItem(itemid));
            actual.Single().Name.Should().Be("nameField");
        }


        [Theory]
        [Foundation.Testing.Attributes.AutoDbData]
        public void GetAll_WrongItemPassed_ShouldThrowException([FakeDb.AutoFixture.Content] Data.Items.Item item, [Frozen] Foundation.Indexing.Repositories.ISearchServiceRepository searchServiceRepository)
        {
            var repository = new MapPointRepository(searchServiceRepository);
            Action a = () => repository.GetAll(item);
            a.Should().Throw<ArgumentException>();
        }
    }
}
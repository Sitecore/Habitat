namespace Sitecore.Feature.Demo.Tests.Models.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using NSubstitute.Extensions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Demo.Models.Repository;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class VisitsRepositoryTests
  {
    private void InitTracker(ITracker tracker)
    {
      tracker.Contact.Returns(Substitute.For<Contact>());
      tracker.Interaction.Returns(Substitute.For<CurrentInteraction>());
      tracker.Contact.System.Returns(Substitute.For<IContactSystemInfoContext>());
    }

    [Theory]
    [AutoDbData]
    public void Get_SeveralVisits_ReturnsTotalVisitsCount(int visitCount, ITracker tracker, [Frozen]IContactProfileProvider contactProfileProvider, VisitsRepository visitsRepository)
    {
      //Arrange
      this.InitTracker(tracker);
      contactProfileProvider.Contact.Returns(x => tracker.Contact);
      tracker.Contact.System.VisitCount.Returns(visitCount);

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var visits = visitsRepository.Get();
        //Assert      
        visits.TotalVisits.Should().Be(visitCount);
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_Value_ReturnsEngagementValue(int value, ITracker tracker, [Frozen]IContactProfileProvider contactProfileProvider, VisitsRepository visitsRepository)
    {
      //Arrange
      this.InitTracker(tracker);
      contactProfileProvider.Contact.Returns(x => tracker.Contact);
      tracker.Contact.System.Value.Returns(value);

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var visits = visitsRepository.Get();
        //Assert      
        visits.EngagementValue.Should().Be(value);
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_PageViews_DoNotReturnCancelledPages(ITracker tracker, [Frozen]IContactProfileProvider contactProfileProvider, VisitsRepository visitsRepository)
    {
      //Arrange
      this.InitTracker(tracker);
      var pages = new List<ICurrentPageContext>(){ this.GeneratePage(false),this.GeneratePage(true),this.GeneratePage(false) };
      tracker.Interaction.GetPages().Returns(pages);
      contactProfileProvider.Contact.Returns(x => tracker.Contact); ;

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var visits = visitsRepository.Get();
        //Assert 
        visits.PageViews.Should().HaveCount(2);
        visits.TotalPageViews.Should().Be(2);
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_PageViews_ReturnReversedPages(ITracker tracker, [Frozen]IContactProfileProvider contactProfileProvider, VisitsRepository visitsRepository)
    {
      //Arrange
      this.InitTracker(tracker);
      var pages = new List<ICurrentPageContext>() { this.GeneratePage(path:"/0"), this.GeneratePage(path: "/1"), this.GeneratePage(path: "/2") };
      tracker.Interaction.GetPages().Returns(pages);
      contactProfileProvider.Contact.Returns(x => tracker.Contact); ;

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var visits = visitsRepository.Get();
        //Assert 
        visits.PageViews.Select(x=>x.Path).Should().BeEquivalentTo(new []{"2", "1", "0"});
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_PageViews_ReturnOnly10Pages(ITracker tracker, [Frozen]IContactProfileProvider contactProfileProvider, VisitsRepository visitsRepository)
    {
      //Arrange
      this.InitTracker(tracker);
      var pages = Enumerable.Repeat(this.GeneratePage(), 15);
      tracker.Interaction.GetPages().Returns(pages);
      contactProfileProvider.Contact.Returns(x => tracker.Contact); ;

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var visits = visitsRepository.Get();
        //Assert 
        visits.PageViews.Should().HaveCount(10);
        visits.TotalPageViews.Should().Be(15);
      }
    }


    private ICurrentPageContext GeneratePage(bool isCancelled = false, string path = null)
    {
      var page = Substitute.For<ICurrentPageContext>();
      page.IsCancelled.Returns(isCancelled);
      page.Url.Returns(new UrlData() { Path = path ?? new Fixture().Create<string>("http://") });
      
      return page;
    }
  }
}

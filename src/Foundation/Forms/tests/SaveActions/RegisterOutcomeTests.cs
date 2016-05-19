namespace Sitecore.Foundation.Forms.Tests.SaveActions
{
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.Foundation.Forms.SaveActions;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class RegisterOutcomeTests
  {
    [Theory]
    [AutoDbData]
    public void Execute_NullOutcome_DoNotRegisterOutcome([Frozen] ITrackerService trackerService, [Greedy] RegisterOutcome registerOutcome)
    {
      //Arrange
      registerOutcome.Outcome = null;

      //Act
      registerOutcome.Execute(ID.Null, null);

      //Assert
      trackerService.DidNotReceive().TrackOutcome(Arg.Any<ID>());
    }

    [Theory]
    [AutoDbData]
    public void Execute_EmptyOutcome_DoNotRegisterOutcome([Frozen] ITrackerService trackerService, [Greedy] RegisterOutcome registerOutcome)
    {
      //Arrange
      registerOutcome.Outcome = string.Empty;

      //Act
      registerOutcome.Execute(ID.Null, null);

      //Assert
      trackerService.DidNotReceive().TrackOutcome(Arg.Any<ID>());
    }

    [Theory]
    [AutoDbData]
    public void Execute_OutcomeIsNotID_DoNotRegisterOutcome(string outcome, [Frozen] ITrackerService trackerService, [Greedy] RegisterOutcome registerOutcome)
    {
      //Arrange
      registerOutcome.Outcome = outcome;

      //Act
      registerOutcome.Execute(ID.Null, null);

      //Assert
      trackerService.DidNotReceive().TrackOutcome(Arg.Any<ID>());
    }

    [Theory]
    [AutoDbData]
    public void Execute_OutcomeIsNotOutcomeID_DoNotRegisterOutcome(Db db, ID outcomeId, ID templateId, [Frozen] ITrackerService trackerService, [Greedy] RegisterOutcome registerOutcome)
    {
      //Arrange
      db.Add(new DbItem("WrongOutcome", outcomeId, templateId));
      registerOutcome.Outcome = outcomeId.ToString();

      //Act
      registerOutcome.Execute(ID.Null, null);

      //Assert
      trackerService.DidNotReceive().TrackOutcome(outcomeId);
    }

    [Theory]
    [AutoDbData]
    public void Execute_OutcomeID_RegisterOutcome(Db db, ID outcomeId, [Frozen] ITrackerService trackerService, [Greedy] RegisterOutcome registerOutcome)
    {
      //Arrange
      db.Add(new DbItem("WrongOutcome", outcomeId, Constants.OutcomeTemplateId));
      registerOutcome.Outcome = outcomeId.ToString();

      //Act
      registerOutcome.Execute(ID.Null, null);

      //Assert
      trackerService.Received().TrackOutcome(outcomeId);
    }
  }
}
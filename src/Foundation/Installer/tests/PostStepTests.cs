namespace Sitecore.Foundation.Installer.Tests
{
  using System.Collections.Specialized;
  using FluentAssertions;
  using log4net.Appender;
  using log4net.Config;
  using log4net.spi;
  using NSubstitute;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class PostStepTests
  {
    [Theory]
    [AutoDbData]
    public void Run_CorrectPostStepAction_CallPostStep(PostStep postStep)
    {
      //Arrange
      FakePostStepAction.CallCount = 0;
      var nameValueCollection = new NameValueCollection();
      nameValueCollection.Add("Attributes", "name1=Sitecore.Foundation.Installer.Tests.PostStepTests+FakePostStepAction, Sitecore.Foundation.Installer.Tests");
      
      //Act
      postStep.Run(null, nameValueCollection);

      //Assert
      FakePostStepAction.CallCount.Should().Be(1);
    }

    [Theory]
    [AutoDbData]
    public void Run_TwoCorrectPostStepAction_CallBothPostSteps(PostStep postStep)
    {
      //Arrange
      FakePostStepAction.CallCount = 0;
      var nameValueCollection = new NameValueCollection();
      nameValueCollection.Add("Attributes", "name1=Sitecore.Foundation.Installer.Tests.PostStepTests+FakePostStepAction, Sitecore.Foundation.Installer.Tests|name2=Sitecore.Foundation.Installer.Tests.PostStepTests+FakePostStepAction, Sitecore.Foundation.Installer.Tests|");

      //Act
      postStep.Run(null, nameValueCollection);

      //Assert
      FakePostStepAction.CallCount.Should().Be(2);
    }

    [Theory]
    [AutoDbData]
    public void Run_NotExistingType_LogError(MemoryAppender appender, PostStep postStep)
    {
      //Arrange
      BasicConfigurator.Configure(appender);
      var nameValueCollection = new NameValueCollection();
      nameValueCollection.Add("Attributes", "NotExistingType");

      //Act
      postStep.Run(null, nameValueCollection);

      //Assert
      appender.Events.Should().Contain(x => x.Level == Level.ERROR && x.RenderedMessage.Contains("NotExistingType"));
    }

    public class FakePostStepAction:IPostStepAction
    {
      public static int CallCount;

      public void Run()
      {
        CallCount++;
      }
    }
  }
}

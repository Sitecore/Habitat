namespace Habitat.Framework.Installer.Tests
{
  using Habitat.Framework.Installer;
  using Habitat.Framework.Installer.XmlTransform;
  using NSubstitute;
  using Xunit;

  public class PostStepTests
  {
    [Theory]
    [AutoSububstituteData]
    public void RunShouldCallXdtTransform(IXdtTransformEngine xdt, IFilePathResolver path)
    {

      var postStep = new PostStep(xdt, path);

      //act
      postStep.Run(null, null);
      xdt.Received(1).ApplyConfigTransformation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
    }


    [Theory]
    [AutoSububstituteData]
    public void RunShouldNotCallXdtTransformIfTransformFileIsMissing(IXdtTransformEngine xdt, IFilePathResolver path)
    {
      path.MapPath(Arg.Any<string>()).Returns((string)null);
      var postStep = new PostStep(xdt, path);

      //act
      postStep.Run(null, null);
      xdt.DidNotReceive().ApplyConfigTransformation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
    }

  }
}
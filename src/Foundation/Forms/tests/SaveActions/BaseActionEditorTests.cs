namespace Sitecore.Foundation.Forms.Tests.SaveActions
{
  using System;
  using System.Collections.Specialized;
  using System.Web;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Foundation.Forms.ActionEditors;
  using Sitecore.Foundation.Forms.Services;
  using Sitecore.Foundation.Testing;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class BaseActionEditorTests
  {
    [Theory]
    [AutoDbData]
    public void ParametersXml_EmptyParametersXml_ReturnEmptyParameters(HttpContext context, [Frozen] ISheerService sheerService)
    {
      //Arrange
      var baseActionEditor = new BaseActionEditorMock(sheerService);
      HttpContext.Current = context;
      //Assert
      baseActionEditor.ParametersMock.Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void ParametersXml_ParametersXmlIsSet_ReturnParameters([Frozen] ISheerService sheerService)
    {
      //Arrange
      var parametersXml = @"<outcome>{9016E456-95CB-42E9-AD58-997D6D77AE83}</outcome>";
      var baseActionEditor = new BaseActionEditorMock(sheerService);

      HttpContext.Current = HttpContextMockFactory.Create(new HttpRequest("", "http://google.com", "params=1"));
      HttpContext.Current.Session["1"] = parametersXml;

      //Assert
      var p = baseActionEditor.ParametersMock["outcome"].Should().Be("{9016E456-95CB-42E9-AD58-997D6D77AE83}");
    }

    [Theory]
    [AutoDbData]
    public void OnOk_EmptyParameters_ReturnDashRsult(HttpContext context, [Frozen] ISheerService sheerService)
    {
      //Arrange
      var baseActionEditor = new BaseActionEditorMock(sheerService);
      HttpContext.Current = context;
      //Act
      baseActionEditor.OnOkMock(this, new EventArgs());

      //Assert
      sheerService.Received().SetDialogValue("-");
    }

    [Theory]
    [AutoDbData]
    public void OnOk_ParametersAreSet_ReturnParametersXml(HttpContext context, [Frozen] ISheerService sheerService)
    {
      //Arrange
      var baseActionEditor = new BaseActionEditorMock(sheerService);
      HttpContext.Current = context;
      baseActionEditor.ParametersMock.Add("outcome", "{00889a5e-81da-459f-8bd8-853983ba7a84}");

      //Act
      baseActionEditor.OnOkMock(this, new EventArgs());

      //Assert
      sheerService.Received().SetDialogValue(HttpUtility.HtmlEncode("<outcome>{00889a5e-81da-459f-8bd8-853983ba7a84}</outcome>"));
    }
  }

  public class BaseActionEditorMock : BaseActionEditor
  {
    public BaseActionEditorMock(ISheerService sheerService) : base(sheerService)
    {
    }

    public NameValueCollection ParametersMock => this.Parameters;

    public void OnOkMock(object sender, EventArgs args)
    {
      this.OnOK(sender, args);
    }
  }
}
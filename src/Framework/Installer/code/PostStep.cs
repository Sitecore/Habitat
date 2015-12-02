namespace Sitecore.Framework.Installer
{
  using System.Collections.Specialized;
  using Sitecore.Framework.Installer.XmlTransform;
  using Sitecore.Install.Framework;

  public class PostStep : IPostStep
  {
    private readonly IXdtTransformEngine xdtTransformEngine;
    private readonly IFilePathResolver filePathResolver;

    public PostStep() : this(new XdtTransformEngine(),new FilePathResolver())
    {
    }

    public PostStep(IXdtTransformEngine xdtTransformEngine, IFilePathResolver filePathResolver)
    {
      this.xdtTransformEngine = xdtTransformEngine;
      this.filePathResolver = filePathResolver;
    }

    public void Run(ITaskOutput output, NameValueCollection metaData)
    {
      var webConfig = this.filePathResolver.MapPath("~/web.config");
      var webConfigTransform = this.filePathResolver.MapPath("~/web.config.transform");
      if (webConfigTransform == null)
      {
        return;
      }

      this.xdtTransformEngine.ApplyConfigTransformation(webConfig, webConfigTransform, webConfig);
    }
  }
}
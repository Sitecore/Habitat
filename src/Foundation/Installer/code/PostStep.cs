namespace Sitecore.Foundation.Installer
{
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Sitecore.Foundation.Installer.XmlTransform;
  using Sitecore.Install.Framework;

  public class PostStep : IPostStep
  {
    private readonly IXdtTransformEngine xdtTransformEngine;
    private readonly IFilePathResolver filePathResolver;
    private readonly ITransformsProvider transformProvider;

    private readonly List<string> transformsOrder = new List<string>()
    {
      "Foundation",
      "Feature",
      "Project"
    };

    public PostStep() : this(new XdtTransformEngine(),new FilePathResolver(), new TransformProvider())
    {
    }

    public PostStep(IXdtTransformEngine xdtTransformEngine, IFilePathResolver filePathResolver, ITransformsProvider transformsProvider)
    {
      this.xdtTransformEngine = xdtTransformEngine;
      this.filePathResolver = filePathResolver;
      this.transformProvider = transformsProvider;
    }

    public void Run(ITaskOutput output, NameValueCollection metaData)
    {
      foreach (var transformsLayer in transformsOrder)
      {
        var transforms = this.transformProvider.GetTransformsByLayer(transformsLayer);
        foreach (var transform in transforms)
        {
          var fileToTransformPath = Regex.Replace(transform, "^.*\\/code", "~").Replace(".transform", "");
          var fileToTransform = this.filePathResolver.MapPath(fileToTransformPath);
          this.ApplyTransform(fileToTransform, transform, fileToTransform);
        }
      }

      var webConfigTransform = this.filePathResolver.MapPath("~/web.config.transform");
      if (webConfigTransform == null)
      {
        return;
      } 
    }

    protected void ApplyTransform(string sourceFilePath, string transformFilePath, string targetFilePath)
    {
      this.xdtTransformEngine.ApplyConfigTransformation(sourceFilePath, transformFilePath, targetFilePath);
    }
  }
}
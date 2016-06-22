namespace Sitecore.Foundation.Installer.XmlTransform
{
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Text.RegularExpressions;
  using Sitecore.Diagnostics;

  public class XmlTransformAction : IPostStepAction
  {
    private readonly IXdtTransformEngine xdtTransformEngine;
    private readonly IFilePathResolver filePathResolver;
    private readonly ITransformsProvider transformProvider;

    private readonly List<string> transformsOrder = new List<string>
                                                    {
                                                      "Foundation",
                                                      "Feature",
                                                      "Project"
                                                    };

    public XmlTransformAction(IXdtTransformEngine xdtTransformEngine, IFilePathResolver filePathResolver, ITransformsProvider transformsProvider)
    {
      this.xdtTransformEngine = xdtTransformEngine;
      this.filePathResolver = filePathResolver;
      this.transformProvider = transformsProvider;
    }

    public XmlTransformAction() : this(new XdtTransformEngine(), new FilePathResolver(), new TransformProvider())
    {
    }

    public void Run(NameValueCollection metaData)
    {
      foreach (var transformsLayer in this.transformsOrder)
      {
        var transforms = this.transformProvider.GetTransformsByLayer(transformsLayer);
        foreach (var transform in transforms)
        {
          var fileToTransformPath = Regex.Replace(transform, "^.*\\\\code", "~").Replace(".transform", "");
          Log.Warn($"{transform} - {fileToTransformPath}", this);
          var fileToTransform = this.filePathResolver.MapPath(fileToTransformPath);
          this.ApplyTransform(fileToTransform, transform, fileToTransform);
        }
      }
    }

    protected void ApplyTransform(string sourceFilePath, string transformFilePath, string targetFilePath)
    {
      this.xdtTransformEngine.ApplyConfigTransformation(sourceFilePath, transformFilePath, targetFilePath);
    }
  }
}
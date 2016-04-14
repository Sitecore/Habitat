namespace Sitecore.Foundation.Installer
{
  using System;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Sitecore.Foundation.Installer.XmlTransform;
  using Sitecore.Install;
  using Sitecore.Install.Files;
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
          var fileToTransformPath = Regex.Replace(transform, "^.*\\\\code", "~").Replace(".transform", "");
          var fileToTransform = this.filePathResolver.MapPath(fileToTransformPath);
          this.ApplyTransform(fileToTransform, transform, fileToTransform);
        }
      }

      this.InstallSecurity(metaData);
    }

    protected void ApplyTransform(string sourceFilePath, string transformFilePath, string targetFilePath)
    {
      this.xdtTransformEngine.ApplyConfigTransformation(sourceFilePath, transformFilePath, targetFilePath);
    }

    protected void InstallSecurity(NameValueCollection metaData)
    {
      if (metaData != null)
      {
        var packageName = $"{metaData["PackageName"]}.zip";
        var installer = new Installer();
        var file = Installer.GetFilename(packageName);
        if (File.Exists(file))
        {
          installer.InstallSecurity(PathUtils.MapPath(file));
        }
      }
    }
  }
}
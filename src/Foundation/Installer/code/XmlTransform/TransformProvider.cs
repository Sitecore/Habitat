using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Installer.XmlTransform
{
  using System.IO;

  public class TransformProvider: ITransformsProvider
  {
    private readonly IFilePathResolver filePathResolver;
    public TransformProvider():this(new FilePathResolver())
    {
    }

    public TransformProvider(IFilePathResolver path)
    {
      this.filePathResolver = path;
    }


    private const string transformsPath = "~/temp/transforms";

    public List<string> GetTransformsByLayer(string layerName)
    {
      var transforms = new List<string>();

      var transformsFolderPath = this.filePathResolver.MapPath(transformsPath);
      if (string.IsNullOrEmpty(transformsFolderPath))
      {
        return transforms;
      }

      var transformsFolder = new DirectoryInfo(transformsFolderPath);

      var layerFolder = transformsFolder.GetDirectories(layerName).FirstOrDefault();
      if (layerFolder != null && layerFolder.Exists)
      {
        transforms.AddRange(layerFolder.GetFiles("*.transform", SearchOption.AllDirectories).Select(transformFile => transformFile.FullName));
      }

      return transforms;
    }
  }
}
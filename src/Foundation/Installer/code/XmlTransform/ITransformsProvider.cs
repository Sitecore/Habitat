namespace Sitecore.Foundation.Installer.XmlTransform
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public interface ITransformsProvider
  {
    List<string> GetTransformsByLayer(string layerName);
  }
}

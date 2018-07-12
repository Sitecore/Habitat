namespace Sitecore.Foundation.Installer.XmlTransform
{
    using System.Collections.Generic;

    public interface ITransformsProvider
    {
        List<string> GetTransformsByLayer(string layerName);
    }
}
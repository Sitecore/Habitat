namespace Sitecore.Foundation.Installer.MongoRestore
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Sitecore.Foundation.Installer.XmlTransform;

  public class MongoPathsProvider : IMongoPathsProvider
  {
    public const string DefaultDumpLocation = "~/App_Data/MongoData";

    private readonly IFilePathResolver filePathResolver;
    private readonly string baseLocation;

    public MongoPathsProvider(IFilePathResolver filePathResolver)
    {
      this.filePathResolver = filePathResolver;
    }

    public MongoPathsProvider():this(new FilePathResolver())
    {
      this.baseLocation = this.filePathResolver.MapPath(DefaultDumpLocation);
    }

    public string GetDumpDirectory(string connectioName) => Path.Combine(this.baseLocation, connectioName);

    public string MongoRestoreExe => Path.Combine(this.baseLocation, "mongorestore.exe");

    public IEnumerable<string> GetDumpNames() => Directory.GetDirectories(this.baseLocation).Select(Path.GetFileName);
  }
}
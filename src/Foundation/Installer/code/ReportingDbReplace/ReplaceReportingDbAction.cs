namespace Sitecore.Foundation.Installer.ReportingDbReplace
{
  using System.Configuration;
  using System.IO;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Installer.XmlTransform;

  public class ReplaceReportingDbAction : IPostStepAction
  {
    private readonly IDatabaseService databaseService;
    private readonly string reportingDbLocation;

    public ReplaceReportingDbAction(IFilePathResolver filePathResolver, IDatabaseService databaseService)
    {
      this.databaseService = databaseService;
      this.reportingDbLocation = filePathResolver.MapPath("~/App_Data/Sitecore.Analytics.mdf");
    }

    public ReplaceReportingDbAction():this(new FilePathResolver(), new DatabaseService())
    {
    }

    public void Run()
    {
      if (File.Exists(this.reportingDbLocation))
      {
        Log.Info("Starting to replace reporting database", this);
        this.databaseService.ReplaceDatabase(ConfigurationManager.ConnectionStrings["reporting"].ConnectionString, this.reportingDbLocation);
      }
    }
  }
}
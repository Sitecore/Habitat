namespace Sitecore.Foundation.LocalDatasource.Services
{
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.LocalDatasource.Extensions;
  using Sitecore.Jobs;

  public class UpdateLocalDatasourceReferencesService
  {
    public UpdateLocalDatasourceReferencesService(Item source, Item target)
    {
      Assert.ArgumentNotNull(source, nameof(source));
      Assert.ArgumentNotNull(target, nameof(target));
      this.Source = source;
      this.Target = target;
    }

    public Item Source { get; }

    public Item Target { get; }

    public void UpdateAsync()
    {
      var jobCategory = typeof(UpdateLocalDatasourceReferencesService).Name;
      var siteName = Context.Site == null ? "No Site Context" : Context.Site.Name;
      var jobOptions = new DefaultJobOptions(this.GetJobName(), jobCategory, siteName, this, nameof(this.Update));
      JobManager.Start(jobOptions);
    }

    private string GetJobName()
    {
      return $"Resolving item references between source {AuditFormatter.FormatItem(this.Source)} and target {AuditFormatter.FormatItem(this.Target)}.";
    }

    public void Update()
    {
      var referenceReplacer = new ItemReferenceReplacer();
      var dependencies = this.Source.GetLocalDatasourceDependencies();
      foreach (var sourceDependencyItem in dependencies)
      {
        var targetDependencyItem = this.GetTargetDependency(sourceDependencyItem);
        if (targetDependencyItem == null)
        {
          Log.Warn($"ChangeLocalDatasourceReferences: Could not resolve {sourceDependencyItem.Paths.FullPath} on {this.Target.Paths.FullPath}", this);
          continue;
        }
        referenceReplacer.AddItemPair(sourceDependencyItem, targetDependencyItem);
      }
      referenceReplacer.ReplaceItemReferences(this.Target);
    }

    private Item GetTargetDependency(Item sourceDependencyItem)
    {
      var sourcePath = sourceDependencyItem.Paths.FullPath;
      var relativePath = sourcePath.Remove(0, this.Source.Paths.FullPath.Length);
      var targetPath = this.Target.Paths.FullPath + relativePath;
      return this.Target.Database.GetItem(targetPath);
    }
  }
}
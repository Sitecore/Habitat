using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Serialization.Infrastructure
{
  using System.Configuration;
  using System.Threading;
  using System.Threading.Tasks;
  using Kamsar.WebConsole;
  using Sitecore.ContentSearch;
  using Sitecore.Data.Managers;
  using Unicorn.Pipelines.UnicornSyncEnd;
  using Unicorn.Publishing;

  public class TriggerIndexRebuild : IUnicornSyncEndProcessor
  {
    public string IndexName { get; set; }

    public void Process(UnicornSyncEndPipelineArgs args)
    {
      //TODO: Check if there are actually any items to reindex

      ISearchIndex index;
      args.Console.ReportStatus($"Initiating reindex of {IndexName}");
      try
      {
        index = ContentSearchManager.GetIndex(IndexName);
      }
      catch (Exception ex)
      {
        args.Console.ReportStatus($"Could not retrieve index {IndexName}", MessageType.Error);
        args.Console.ReportException(ex);
        return;
      }
      args.Console.ReportStatus($"Index found, {index.Summary.NumberOfDocuments} documents in index before reindex.");
      index.Rebuild(IndexingOptions.Default);
      args.Console.ReportStatus($" Completed reindex of {IndexName}: {index.Summary.NumberOfDocuments} documents updated.");
    }
  }
}
namespace Sitecore.Foundation.Testing.Pipelines
{
  using Sitecore.FakeDb.Pipelines;
  using Sitecore.Pipelines;

  public class EmptyPipeline : IPipelineProcessor
  {
    public void Process(PipelineArgs args)
    {
    }
  }
}
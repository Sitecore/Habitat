namespace Sitecore.Foundation.Testing.Builders
{
  using System.Reflection;
  using Ploeh.AutoFixture.Kernel;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.Pipelines;
  using Sitecore.Foundation.Testing.Attributes;

  public class ResolvePipelineSpecimenBuilder : AttributeRelay<ResolvePipelineAttribute>
  {
    protected override object Resolve(ISpecimenContext context, ResolvePipelineAttribute attribute, ParameterInfo parameterInfo)
    {
      var db = (Db)context.Resolve(typeof(Db));
      db.PipelineWatcher.Dispose();
      db.GetType().GetField("pipelineWatcher", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(db, null);
      var pipeline = (IPipelineProcessor)base.Resolve(context, attribute, parameterInfo);
      db.PipelineWatcher.Register(attribute.PipelineName, pipeline);
      return pipeline;
    }
  }
}
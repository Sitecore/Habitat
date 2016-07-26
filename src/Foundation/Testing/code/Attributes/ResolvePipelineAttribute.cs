namespace Sitecore.Foundation.Testing.Attributes
{
  using System;

  public class ResolvePipelineAttribute : Attribute
  {
    public string PipelineName { get; set; }

    public ResolvePipelineAttribute(string pipelineName)
    {
      this.PipelineName = pipelineName;
    }

    public ResolvePipelineAttribute()
    {
    }
  }
}
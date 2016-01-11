namespace UnitTests.Common.Attributes
{
  using System;

  public class ResolvePipelineAttribute : Attribute
  {
    public string PipelineName { get; set; }

    public ResolvePipelineAttribute(string pipelineName)
    {
      PipelineName = pipelineName;
    }
  }
}
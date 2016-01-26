using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Common.Pipelines
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

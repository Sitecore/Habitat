using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Habitat.Framework.SitecoreExtensions.Repositories;
using Habitat.Framework.SitecoreExtensions.Tests.Common;
using Sitecore.Data;
using Sitecore.FakeDb;
using Xunit;

namespace Habitat.Framework.SitecoreExtensions.Tests.Repositories
{
  public class DatabaseRepositoryTests
  {
    [Theory, AutoDbData]
    public void ShouldReturnMasterDB()
    {
      DatabaseRepository.GetActiveDatabase().Name.Should().Be("master");

    }

  }
}

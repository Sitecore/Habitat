namespace Sitecore.Foundation.SynthesisModeling
{
	using System;

	public abstract class ProjectModelRegistration : HabitatModelRegistration
	{
		protected override string HabitatModuleType => "Project";

		protected override string HabitatModuleName
		{
			get
			{
				// normally we want the last segment e.g. Sitecore.Foundation.Multisite
				// but projects break that mold and use e.g. Sitecore.Common.Website and the middle element is the name of the component
				var projectName = this.ConfigurationName.Replace(".Website", string.Empty);

				return projectName.Substring(projectName.LastIndexOf(".", StringComparison.Ordinal) + 1);
			}
		}
	}
}

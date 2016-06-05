namespace Sitecore.Foundation.SynthesisModeling
{
	using System;
	using System.Collections.Generic;
	using Synthesis.Configuration.Registration;

	public abstract class HabitatModelRegistration : SynthesisConfigurationRegistration
	{
		protected abstract string HabitatModuleType { get; }

		protected override string ConfigurationName => this.GetType().Assembly.GetName().Name;

		protected string HabitatModuleName => this.ConfigurationName.Substring(this.ConfigurationName.LastIndexOf(".", StringComparison.Ordinal) + 1);

		protected override IEnumerable<string> IncludedTemplates
		{
			get { yield return $"/sitecore/Templates/{this.HabitatModuleType}/{this.HabitatModuleName}"; }
		}

		protected override string NamespaceTemplatePathRoot => $"/sitecore/templates";

		protected override string ModelOutputFilePath => $"{this.HabitatModuleType}/{this.HabitatModuleName}/code/Models/Synthesis.Model.cs";

		protected override string RootGeneratedNamespace => $"{this.ConfigurationName}.Models";
	}
}

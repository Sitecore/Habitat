/*************************************

DO NOT CHANGE THIS FILE - UPDATE GlassMapperScCustom.cs

**************************************/

using Glass.Mapper.Maps;
using Glass.Mapper.Sc.Configuration.Fluent;
using Glass.Mapper.Sc.IoC;
using Sitecore.Pipelines;

// WebActivator has been removed. If you wish to continue using WebActivator uncomment the line below
// and delete the Glass.Mapper.Sc.CastleWindsor.config file from the Sitecore Config Include folder.
// [assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Sitecore.Habitat.Website.App_Start.GlassMapperSc), "Start")]

namespace Sitecore.Habitat.Website.App_Start
{
	public class  GlassMapperSc
	{
		public void Process(PipelineArgs args){
			GlassMapperSc.Start();
		}

		public static void Start()
		{
			//install the custom services
			var resolver = GlassMapperScCustom.CreateResolver();

			//create a context
			var context = Glass.Mapper.Context.Create(resolver);

			LoadConfigurationMaps(resolver, context);

			context.Load(      
				GlassMapperScCustom.GlassLoaders()        				
				);

			GlassMapperScCustom.PostLoad();
		}

        public static void LoadConfigurationMaps(IDependencyResolver resolver, Glass.Mapper.Context context)
        {
            var dependencyResolver = resolver as DependencyResolver;
            if (dependencyResolver == null)
            {
                return;
            }

            if (dependencyResolver.ConfigurationMapFactory is ConfigurationMapConfigFactory)
            {
                GlassMapperScCustom.AddMaps(dependencyResolver.ConfigurationMapFactory);
            }

            IConfigurationMap configurationMap = new ConfigurationMap(dependencyResolver);
            SitecoreFluentConfigurationLoader configurationLoader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();
            context.Load(configurationLoader);
        }
	}
}
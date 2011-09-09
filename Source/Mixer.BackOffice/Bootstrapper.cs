using Mixer.BackOffice.Infrastructure.Nancy;
using MixerUI.BackOffice.Infrastructure;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;

namespace Mixer.BackOffice
{
    public class Bootstrapper : StructureMapNancyBootstrapper
    {
        #region Methods
        protected override void InitialiseInternal(IContainer container)
        {
            base.InitialiseInternal(container);

            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "~/login",
                    UsernameMapper = container.GetInstance<IUsernameMapper>()
                };

            FormsAuthentication.Enable(this, formsAuthConfiguration);
        }
        protected override void ConfigureApplicationContainer(IContainer existingContainer)
        {
            existingContainer.Configure(registry =>
            {
                registry.Scan(x =>
                {
                    x.AssemblyContainingType<StructureMapNancyBootstrapper>();
                    x.AssembliesFromApplicationBaseDirectory();
                    x.LookForRegistries();
                    x.AddAllTypesOf<IRequireConfigurationOnStartup>();
                });
            });

            ConfigureOnStartup(existingContainer);
        }
        public void ConfigureOnStartup(IContainer container)
        {
            var dependenciesToInitialized = container.GetAllInstances<IRequireConfigurationOnStartup>();
            foreach (var dependency in dependenciesToInitialized)
            {
                dependency.Configure();
            }
        }
        #endregion

        #region Properties
        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(cfg => 
                {
                    cfg.ViewFactory = typeof(ConfigurableModelViewFactory);
                    cfg.NancyModuleBuilder = typeof(MixerModuleBuilder);
                });
            }
        }
        #endregion
    }
}
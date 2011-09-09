using Mixer.BackOffice.Infrastructure.Authentication;
using Mixer.BackOffice.Infrastructure.Nancy;
using Nancy.Authentication.Forms;
using Nancy.ViewEngines.Razor;
using StructureMap.Configuration.DSL;

namespace Mixer.BackOffice.Infrastructure.Dependencies
{
    public class NancyRegistry : Registry
    {
        public NancyRegistry()
        {
            For<IUsernameMapper>().Use<UsernameMapper>();
            For<IRazorConfiguration>().Singleton().Use<RazorConfiguration>();
        }
    }
}
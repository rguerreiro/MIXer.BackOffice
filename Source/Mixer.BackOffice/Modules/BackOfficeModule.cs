using Mixer.BackOffice.Infrastructure.Nancy;
using Nancy.Security;

namespace Mixer.BackOffice.Modules
{
    public class BackOfficeModule : MixerModule
    {
        public BackOfficeModule()
            : base("/backoffice")
        {
            this.RequiresAuthentication();

            Get["/"] = _ =>
            {
                return View["BackOfficeIndex"];
            };
        }
    }
}
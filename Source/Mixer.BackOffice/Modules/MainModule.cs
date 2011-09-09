using Mixer.BackOffice.Infrastructure.Nancy;
using Nancy;

namespace Mixer.BackOffice.Modules
{
    public class MainModule : MixerModule
    {
        public MainModule()
        {
            Get["/"] = _ => View["Index"];

            Get["/css/{CssFile}"] = req =>
            {
                return Response.AsCss("Public/css/" + (string)req.CssFile);
            };
        }
    }
}
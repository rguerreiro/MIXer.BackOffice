using System;
using Nancy;

namespace Mixer.BackOffice.Infrastructure.Nancy
{
    public abstract class MixerModule : NancyModule, ISubscribeModuleEvents
    {
        protected MixerModule()
            : base()
        {
        }

        protected MixerModule(string modulePath)
            : base(modulePath)
        {
        }

        public void ModuleCreated()
        {
            var viewFactory = this.ViewFactory as ISubscribePreRender;

            if (viewFactory != null)
            {
                viewFactory.SubscribePreRender(this, (viewName, model, viewLocationContext) =>
                {
                    model.Username = !viewLocationContext.Context.Items.ContainsKey("username") ? null : viewLocationContext.Context.Items["username"] as string;
                    model.IsAuthenticated = viewLocationContext.Context.Items.ContainsKey("username") && !String.IsNullOrEmpty(viewLocationContext.Context.Items["username"] as string);
                });
            }
        }

        public bool IsAuthenticated 
        {
            get { return Context.Items.ContainsKey("username") && !String.IsNullOrEmpty(Context.Items["username"] as string); } 
        }
        public string Username 
        {
            get { return !Context.Items.ContainsKey("username") ? null : Context.Items["username"] as string; }
        }
    }
}
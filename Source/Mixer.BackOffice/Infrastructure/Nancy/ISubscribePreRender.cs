using System;
using Nancy;
using Nancy.ViewEngines;

namespace Mixer.BackOffice.Infrastructure.Nancy
{
    public interface ISubscribePreRender
    {
        void SubscribePreRender(NancyModule module, Action<string, dynamic, ViewLocationContext> callback);
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Nancy;
using Nancy.ViewEngines;
using System.Linq;

namespace Mixer.BackOffice.Infrastructure.Nancy
{
    public class ConfigurableModelViewFactory : IViewFactory, ISubscribePreRender
    {
        #region Fields
        private Dictionary<string, Action<string, dynamic, ViewLocationContext>> _preRenderSubscribers = new Dictionary<string, Action<string, dynamic, ViewLocationContext>>();
        private DefaultViewFactory _viewFactory;
        #endregion

        #region Constructors
        public ConfigurableModelViewFactory(IViewResolver viewResolver, IEnumerable<IViewEngine> viewEngines, IRenderContextFactory renderContextFactory)
        {
            _viewFactory = new DefaultViewFactory(viewResolver, viewEngines, renderContextFactory);
        }
        #endregion

        #region Methods
        public void SubscribePreRender(NancyModule module, Action<string, dynamic, ViewLocationContext> callback)
        {
            if (!_preRenderSubscribers.ContainsKey(module.ModulePath))
                _preRenderSubscribers.Add(module.ModulePath, callback);
        }
        public Action<Stream> RenderView(string viewName, dynamic model, ViewLocationContext viewLocationContext)
        {
            if (model == null)
                model = new ExpandoObject();

            _preRenderSubscribers
                .Where(x => x.Key == viewLocationContext.ModulePath)
                .Select(x => x.Value)
                .ToList()
                .ForEach(x => x.Invoke(viewName, model, viewLocationContext));

            return _viewFactory.RenderView(viewName, model, viewLocationContext);
        }
        #endregion
    }
}
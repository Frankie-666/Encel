using System.Web.Mvc;
using System.Web.Routing;
using Encel.Commands;
using Encel.Content.Abstractions;
using Encel.ContentResolver;
using Encel.Messaging;

namespace Encel.Mvc
{
    public class MvcStartupConsumer : IConsumer<AppStartupCommand>
    {
        private IContentRepository _contentRepository;
        private IContentControllerResolver _contentControllerResolver;

        public void Consume(AppStartupCommand command)
        {
            var contentRepositoryFactory = EncelApplication.Configuration.ContentRepositoryFactory;

            _contentRepository = contentRepositoryFactory.Create();
            _contentControllerResolver = EncelApplication.Configuration.ContentControllerResolver;

            RegisterRoutes(RouteTable.Routes);

            ModelBinderProviders.BinderProviders.Add(new ContentModelBinderProvider());
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            var constraint = new ContentRouteConstraint(_contentRepository, _contentControllerResolver);
            var url = "{*node}";
            var rootPath = EncelApplication.Configuration.AppSettings.RootPath;

            if (!string.IsNullOrEmpty(rootPath))
            {
                url = rootPath.Trim('/') + "/" + url;
            }

            routes.MapRoute(
                "Encel-Default",
                url,
                new { node = "", action = "Index" },
                new { node = constraint }
            );
        }
    }
}
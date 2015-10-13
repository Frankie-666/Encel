using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Encel.Content;
using Encel.Content.Abstractions;
using Encel.ContentResolver;

namespace Encel.Mvc
{
    public class ContentRouteConstraint : IRouteConstraint
    {
        private readonly IContentRepository _contentRepository;
        private readonly IContentControllerResolver _contentControllerResolver;

        public ContentRouteConstraint(IContentRepository contentRepository, IContentControllerResolver contentControllerResolver)
        {
            _contentRepository = contentRepository;
            _contentControllerResolver = contentControllerResolver;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // TODO handle urlGeneration
            var nodeValue = values[parameterName];

            if (nodeValue == null)
            {
                return false;
            }
            
            string action = null;
            var currentContentUri = new Uri(ContentUri.BaseUri, nodeValue.ToString());
            var currentContent = _contentRepository.Get(currentContentUri);

            if (currentContent == null && currentContentUri != ContentUri.BaseUri)
            {
                action = currentContentUri.Segments.Last();

                currentContentUri = ContentUri.GetParent(currentContentUri);
                currentContent = _contentRepository.Get(currentContentUri);
            }

            // TODO: also check if user is logged in and SHOULD see unpublished pages
            if (currentContent == null || (!currentContent.IsPublished && !httpContext.IsDebuggingEnabled))
            {
                return false;
            }

            var layoutMetaData = _contentControllerResolver.Resolve(currentContent.GetType());

            if (layoutMetaData != null)
            {
                // TODO increase performance of this section
                if (action != null)
                {
                    var actionMethods = layoutMetaData.ControllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                    var hasAction = actionMethods.Any(m => m.Name.Equals(action, StringComparison.InvariantCultureIgnoreCase)); // this won't take ActionName attributes in consideration.

                    // controller doesn't have a matching action so we won't route through cms.
                    if (!hasAction)
                    {
                        return false;
                    }
                }

                values["controller"] = layoutMetaData.ControllerName;
                values["action"] = action ?? layoutMetaData.ActionName;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Could not find suitable controller for content type \"{0}\". \r\nCreate a new controller named \"{1}Controller\", or use the [ContentController(typeof({1}))] attribute on a controller that should be used.", currentContent.Layout, currentContent.GetType().Name));
            }

            values[MvcContextExtensions.CURRENT_CONTENT_KEY] = currentContent;
            return true;
        }
    }
}

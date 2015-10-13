using System.Web.Mvc;
using System.Web.Routing;
using Encel.Models;

namespace Encel.Mvc
{
    public static class MvcContextExtensions
    {
        public const string CURRENT_CONTENT_KEY = "currentContent";

        public static IContentData GetCurrentContent(this RequestContext viewContext)
        {
            return viewContext.RouteData.Values[CURRENT_CONTENT_KEY] as IContentData;
        }

        public static IContentData GetCurrentContent(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values[CURRENT_CONTENT_KEY] as IContentData;
        }

        public static void SetCurrentContent(this RequestContext viewContext, IContentData contentData)
        {
            viewContext.RouteData.Values[CURRENT_CONTENT_KEY] = contentData;
        }

        public static void SetCurrentContent(this ViewContext viewContext, IContentData contentData)
        {
            viewContext.RouteData.Values[CURRENT_CONTENT_KEY] = contentData;
        }
    }
}

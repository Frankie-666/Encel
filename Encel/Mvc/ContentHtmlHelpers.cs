using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Encel.Mvc
{
    public static class ContentHtmlHelpers
    {
        public static IHtmlString ContentFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var propertyValue = expression.Compile().Invoke(html.ViewData.Model);

            if (propertyValue == null)
            {
                return new HtmlString(string.Empty);
            }

            var content = propertyValue.ToString();
            var transformers = EncelApplication.Configuration.ContentTransformers;

            if (transformers != null)
            {
                var contentTransformers = transformers.OrderBy(ct => ct.Priority);

                foreach (var contentTransformer in contentTransformers)
                {
                    content = contentTransformer.Transform(content);
                }
            }

            return new HtmlString(content);
        }
    }
}

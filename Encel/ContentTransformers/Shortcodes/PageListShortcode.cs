using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Encel.Content;
using Encel.Models;
using Encel.Mvc;
using Shortcoder;

namespace Encel.ContentTransformers.Shortcodes
{
    public class PageListShortcode : Shortcode
    {
        public static string ListCssClass = "List";
        public static string ListItemCssClass = "List-item";
        public static string ListItemLinkCssClass = "";

        public string Root { get; set; }
        public int? Take { get; set; }
        public string OfType { get; set; }
        public bool Descendants { get; set; }

        public override string Generate(IShortcodeContext context)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("<ul class=\"{0}\">", ListCssClass);

            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            var currentContent = HttpContext.Current.Request.RequestContext.GetCurrentContent();
            var contentUri = Root != null ? ContentUri.Parse(Root) : currentContent.ContentUri;

            IEnumerable<IContentData> children;

            if (Descendants)
            {
                children = contentRepository.GetDescendants(contentUri);
            }
            else
            {
                children = contentRepository.GetChildren(contentUri);
            }

            children = children.OrderBy(m => m.Published).AsEnumerable();

            if (IsSet("OfType"))
            {
                children = children.Where(m => m.Layout != null && m.Layout.Equals(OfType, StringComparison.InvariantCultureIgnoreCase));
            }

            if (Take.HasValue)
            {
                children = children.Take(Take.GetValueOrDefault());
            }

            foreach (var contentData in children)
            {
                sb.AppendFormat("<li class=\"{2}\"><a href=\"{0}\">{1}</a></li>", contentData.ContentUri.AbsolutePath, contentData.Title ?? contentData.Slug, ListItemCssClass);
            }

            sb.Append("</ul>");

            return sb.ToString();
        }
    }
}
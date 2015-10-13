using System;
using System.Web;
using System.Web.Mvc;
using Shortcoder;

namespace Encel.ContentTransformers.Shortcodes
{
    public class YoutubeShortcode : Shortcode
    {
        private const string YOUTUBE_URL_FORMAT = "http://www.youtube.com/embed/{0}?enablejsapi=1&origin={1}";

        public string Width { get; set; }
        public string Height { get; set; }
        public string Id { get; set; }

        public YoutubeShortcode()
        {
            Width = "640";
            Height = "390";
        }

        public override string Generate(IShortcodeContext context)
        {
            TagBuilder html = new TagBuilder("iframe");

            html.MergeAttribute("width", Width);
            html.MergeAttribute("height", Height);
            html.MergeAttribute("type", "text/html");
            html.MergeAttribute("frameborder", "0");
            html.MergeAttribute("src", string.Format(YOUTUBE_URL_FORMAT, Id, HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)));

            return html.ToString(TagRenderMode.Normal) + Content;
        }
    }
}
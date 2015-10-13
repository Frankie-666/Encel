using System.Text.RegularExpressions;

namespace Encel.ContentTransformers.Markdown
{
    public class MarkdownConverter
    {
        const string CODE_BLOCK_WITH_LANGUAGE = "<pre><code class=\"{0}\">{1}</code></pre>";
        const string CODE_BLOCK = "<pre><code>{0}</code></pre>";
        const string CODE_LANGUAGE_PATTERN = @"\{language-(\w+)\}";
        const string WHITESPACE_PATTERN = @"\s*";

        private MarkdownDeep.Markdown _markdown;

        public MarkdownConverter()
        {
            _markdown = new MarkdownDeep.Markdown
            {
                ExtraMode = true,
                HtmlClassTitledImages = "Figure",
                FormatCodeBlock = FormatCodeBlock,
            };
        }

        public virtual string GetFileExtension()
        {
            return "md";
        }

        public virtual string Convert(string source)
        {
            return _markdown.Transform(source);
        }

        private string FormatCodeBlock(MarkdownDeep.Markdown markdown, string code)
        {
            var match = Regex.Match(code, CODE_LANGUAGE_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (match.Success)
            {
                // get the language name
                var language = match.Groups[1].Value;

                // remove language tag from code
                code = Regex.Replace(code, WHITESPACE_PATTERN + CODE_LANGUAGE_PATTERN + WHITESPACE_PATTERN, string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                // wrap code in pre and code tags with correct class name
                return string.Format(CODE_BLOCK_WITH_LANGUAGE, "language-" + language, code);
            }

            return string.Format(CODE_BLOCK, code);
        }
    }
}
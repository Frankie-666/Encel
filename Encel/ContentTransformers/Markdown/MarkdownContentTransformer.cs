namespace Encel.ContentTransformers.Markdown
{
    public class MarkdownContentTransformer : IContentTransformer
    {
        public int Priority { get { return 2000; } }

        public string Transform(string content)
        {
            var converter = new MarkdownConverter();
            return converter.Convert(content);
        }
    }
}
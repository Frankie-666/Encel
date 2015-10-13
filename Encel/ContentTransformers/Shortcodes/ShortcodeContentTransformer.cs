using Shortcoder;

namespace Encel.ContentTransformers.Shortcodes
{
    public class ShortcodeContentTransformer : IContentTransformer
    {
        public int Priority { get { return 1000; } }

        public string Transform(string content)
        {
            return ShortcodeParser.Current.Parse(content);
        }
    }
}
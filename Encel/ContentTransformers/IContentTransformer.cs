namespace Encel.ContentTransformers
{
    public interface IContentTransformer
    {
        int Priority { get; }

        string Transform(string content);
    }
}
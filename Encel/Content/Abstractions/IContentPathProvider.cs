namespace Encel.Content.Abstractions
{
    public interface IContentPathProvider
    {
        string ContentDirectoryPath { get; set; }
        string FileExtension { get; set; }
    }
}
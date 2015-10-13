using Encel.Content.Abstractions;

namespace Encel.Content
{
    public class ContentPathProvider : IContentPathProvider
    {
        public ContentPathProvider(string contentDirectoryPath, string fileExtension)
        {
            ContentDirectoryPath = contentDirectoryPath;
            FileExtension = fileExtension;
        }

        public string ContentDirectoryPath { get; set; }
        public string FileExtension { get; set; }
    }
}
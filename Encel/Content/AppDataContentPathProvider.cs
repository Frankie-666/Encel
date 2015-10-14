using System;
using Encel.Content.Abstractions;

namespace Encel.Content
{
    public class AppDataContentPathProvider : IContentPathProvider
    {
        public AppDataContentPathProvider()
        {
            var contentDirectoryPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            
            ContentDirectoryPath = contentDirectoryPath;
            FileExtension = "md";
        }

        public string ContentDirectoryPath { get; set; }
        public string FileExtension { get; set; }
    }
}
using System;
using System.IO;

namespace Encel.Content
{
    public class ContentUri
    {
        public static readonly Uri BaseUri = new Uri("cms://root");

        public static Uri Parse(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return null;
            }

            return new Uri(BaseUri, relativePath.Trim('/'));
        }

        public static Uri GetParent(Uri contentUri)
        {
            var isDirectory = contentUri.LocalPath.EndsWith("/");
            var parentContentUri = new Uri(contentUri, isDirectory ? ".." : ".");

            if (parentContentUri == contentUri)
            {
                return null;
            }

            return parentContentUri;
        }

        public static string GetAbsolutePath(Uri contentUri, bool addFileExtension = false)
        {
            var contentPathProvider = EncelApplication.Configuration.ContentPathProvider;
            var absolutePath = Path.Combine(contentPathProvider.ContentDirectoryPath, contentUri.LocalPath.Trim('/'));

            if (addFileExtension)
            {
                if (contentUri == ContentUri.BaseUri)
                {
                    return Path.Combine(contentPathProvider.ContentDirectoryPath, "index.md");
                }
                
                absolutePath = Path.ChangeExtension(absolutePath, contentPathProvider.FileExtension);
            }

            return absolutePath;
        }
    }
}

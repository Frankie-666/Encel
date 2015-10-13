using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Encel.Content.Abstractions;
using Encel.ContentResolver;
using Encel.Models;
using Encel.Utilities;

namespace Encel.Content
{
    public class FileContentRepository : IContentRepository
    {
        private readonly string _contentDirectoryPath;
        private readonly Uri _contentDirectoryUri;
        private readonly string _fileExtension;
        private readonly IContentSerializerFactory _contentSerializerFactory;

        public FileContentRepository(IContentPathProvider contentPathProvider)
        {
            _contentDirectoryPath = contentPathProvider.ContentDirectoryPath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? contentPathProvider.ContentDirectoryPath : contentPathProvider.ContentDirectoryPath + Path.DirectorySeparatorChar;
            _contentDirectoryUri = new Uri(_contentDirectoryPath);
            _fileExtension = contentPathProvider.FileExtension;
            _contentSerializerFactory = EncelApplication.Configuration.ContentSerializerFactory;
        }

        public virtual TContentData Get<TContentData>(Uri contentUri) where TContentData : class, IContentData
        {
            if (contentUri == null)
                return null;

            var path = ContentUri.GetAbsolutePath(contentUri);

            var folderPath = Path.GetDirectoryName(path);
            bool directoryExists = Directory.Exists(folderPath);
            
            if (!directoryExists)
            {
                return null;
            }

            var contentName = Path.GetFileNameWithoutExtension(path);
            var contentFilePath = FindContentFilePath(folderPath, contentName);

            if (contentFilePath == null)
            {
                // return FolderContentData if it's found, but not for startpage
                if (Directory.Exists(path) && contentUri != ContentUri.BaseUri) { 
                    return GetContentDataFromFile<TContentData>(contentUri, false, path, null);
                }

                return null;
            }

            return GetContentDataFromFile<TContentData>(contentUri, true, path, contentFilePath);
        }

        public virtual IEnumerable<Uri> GetChildrenReferences(Uri contentUri)
        {
            return GetChildrenFromDirectory(contentUri, SearchOption.TopDirectoryOnly);
        }

        public virtual IEnumerable<Uri> GetDescendantReferences(Uri contentUri)
        {
            return GetChildrenFromDirectory(contentUri, SearchOption.AllDirectories);
        }

        public virtual IEnumerable<Uri> GetAncestorReferences(Uri contentUri)
        {
            var parentContentUri = ContentUri.GetParent(contentUri);

            while (parentContentUri != null)
            {
                yield return NormalizeContentUri(parentContentUri);

                parentContentUri = ContentUri.GetParent(parentContentUri);
            }
        }

        public virtual IEnumerable<Uri> GetSiblingReferences(Uri contentUri)
        {
            contentUri = NormalizeContentUri(contentUri);
            var parentContentUri = ContentUri.GetParent(contentUri);

            return GetChildrenReferences(parentContentUri).Where(uri => uri != contentUri);
        }

        public Uri Save(IContentData contentData)
        {
            IContentTypeProvider contentTypeProvider = new ContentTypeProvider();
            IContentSerializer contentSerializer = new FrontMatterContentSerializer(contentTypeProvider);

            if (string.IsNullOrEmpty(contentData.Layout))
            {
                contentData.Layout = contentData.GetType().Name;
            }

            var fileContent = contentSerializer.Serialize(contentData);

            string contentPath;
            if (contentData.ContentUri != null)
            {
                // if the slug has been updated, rename the file
                if (!contentData.ContentUri.Segments.Last().Equals(contentData.Slug, StringComparison.InvariantCultureIgnoreCase))
                {
                    contentData.ContentUri = RenameContentAssets(contentData.ContentUri, contentData.Slug);
                }

                contentPath = ContentUri.GetAbsolutePath(contentData.ContentUri, true);
            }
            else if (contentData.ParentUri != null)
            {
                var parentPath = ContentUri.GetAbsolutePath(contentData.ParentUri);
                
                if (string.IsNullOrEmpty(contentData.Slug) && string.IsNullOrEmpty(contentData.Title))
                {
                    throw new InvalidOperationException("Could not create a URL slug for content. Please supply either Title or Slug to save the content.");
                }
                
                if (string.IsNullOrEmpty(contentData.Slug))
                {
                    contentData.Slug = contentData.Title.ToUrlSlug();
                }

                contentPath = Path.Combine(parentPath, Path.ChangeExtension(contentData.Slug, _fileExtension));
            }
            else
            {
                throw new InvalidOperationException("Cannot save content when both ParentUri and ContentUri is missing.");
            }

            var directoryPath = Path.GetDirectoryName(contentPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(contentPath, fileContent);

            return NormalizeContentUri(new Uri(contentPath));
        }

        public Uri Move(Uri contentUri, Uri parentDestinationUri)
        {
            var path = ContentUri.GetAbsolutePath(contentUri);
            var destinationPath = ContentUri.GetAbsolutePath(parentDestinationUri);
            var filePath = Path.ChangeExtension(path, _fileExtension);
            var fileExists = File.Exists(filePath);
            var directoryExists = Directory.Exists(path);

            if (!fileExists && !directoryExists)
            {
                throw new ArgumentException("Content does not exist.", "contentUri");
            }

            if (!Directory.Exists(destinationPath))
            {
                throw new ArgumentException("Destination folder does not exist.", "parentDestinationUri");
            }

            if (fileExists)
            {
                File.Move(filePath, Path.Combine(destinationPath, Path.GetFileName(filePath)));
            }

            var finalDestinationPath = Path.Combine(destinationPath, Path.GetFileName(path));

            if (directoryExists)
            {
                Directory.Move(path, finalDestinationPath);
            }

            return NormalizeContentUri(new Uri(finalDestinationPath));
        }

        public void Delete(Uri contentUri, bool deleteChildren = true)
        {
            var path = ContentUri.GetAbsolutePath(contentUri);
            var filePath = Path.ChangeExtension(path, _fileExtension);
            var fileExists = File.Exists(filePath);
            var directoryExists = Directory.Exists(path);

            if (!fileExists && !directoryExists)
            {
                return;
            }

            if (fileExists)
            {
                File.Delete(filePath);
            }

            if (deleteChildren && directoryExists)
            {
                Directory.Delete(path + Path.DirectorySeparatorChar, true);
            }
        }

        private string FindContentFilePath(string folderPath, string contentName)
        {
            var filePath1 = Path.Combine(folderPath, contentName + "." + _fileExtension); // /folderPath/contentName.md
            var filePath2 = Path.Combine(folderPath, contentName, "index." + _fileExtension); // /folderPath/contentName/index.md

            if (File.Exists(filePath1))
                return filePath1;

            if (File.Exists(filePath2))
                return filePath2;

            return null;
        }

        private TContentData GetContentDataFromFile<TContentData>(Uri contentUri, bool fileExists, string path, string filePath)
            where TContentData : class, IContentData
        {
            IContentData contentData;

            if (fileExists)
            {
                var contentSerializer = _contentSerializerFactory.Create();

                path = filePath;
                contentData = contentSerializer.Deserialize<TContentData>(filePath);
            }
            else
            {
                contentData = new FolderContentData
                {
                    Title = Path.GetFileName(path)
                };
            }

            contentData.ContentUri = NormalizeContentUri(contentUri);
            contentData.Slug = Path.GetFileNameWithoutExtension(path);
            contentData.ParentUri = ContentUri.GetParent(contentData.ContentUri);

            if (contentData.Created == DateTime.MinValue)
                contentData.Created = File.GetCreationTimeUtc(path);

            if (contentData.Modified == DateTime.MinValue)
                contentData.Modified = File.GetLastWriteTimeUtc(path);

            return (TContentData)contentData;
        }

        private Uri RenameContentAssets(Uri contentUri, string newSlug)
        {
            var directoryPath = ContentUri.GetAbsolutePath(contentUri);
            var filePath = Path.ChangeExtension(directoryPath, _fileExtension);
            var parentDirectoryPath = ContentUri.GetAbsolutePath(ContentUri.GetParent(contentUri));

            string newFilePath = Path.Combine(parentDirectoryPath, Path.ChangeExtension(newSlug, _fileExtension)),
                   newDirectoryPath = Path.Combine(parentDirectoryPath, newSlug);

            bool directoryExists = Directory.Exists(directoryPath),
                 fileExists = File.Exists(filePath);

            if (!directoryExists && !fileExists)
            {
                return contentUri;
            }

            if (fileExists)
            {
                if (File.Exists(newFilePath))
                {
                    throw new InvalidOperationException("Cannot rename content slug to a file that already exists.");
                }

                File.Move(filePath, newFilePath);
            }

            if (directoryExists)
            {
                if (Directory.Exists(newDirectoryPath))
                {
                    throw new InvalidOperationException("Cannot rename content slug to a directory that already exists.");
                }

                Directory.Move(directoryPath, newDirectoryPath);
            }

            return NormalizeContentUri(new Uri(newFilePath));
        }

        private IEnumerable<Uri> GetChildrenFromDirectory(Uri contentUri, SearchOption searchOption)
        {
            var directoryPath = ContentUri.GetAbsolutePath(contentUri);

            if (!Directory.Exists(directoryPath))
            {
                yield break;
            }

            var childEntries = Directory.EnumerateFileSystemEntries(directoryPath, "*", searchOption)
                .Where(IsSupportedFileOrDirectory)
                .Select(path => Path.ChangeExtension(path, null))
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Where(path => !path.EndsWith("index"));

            foreach (var filePath in childEntries)
            {
                yield return NormalizeContentUri(new Uri(filePath));
            }
        }

        private bool IsSupportedFileOrDirectory(string path)
        {
            var extension = Path.GetExtension(path);
            
            if (!string.IsNullOrEmpty(extension))
            {
                extension = extension.Replace(".", string.Empty);
                return _fileExtension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
            }

            return true;
        }

        private Uri NormalizeContentUri(Uri uri)
        {
            if (uri == null)
            {
                return null;
            }

            if (uri.IsFile)
            {
                var relativePath = Path.ChangeExtension(_contentDirectoryUri.MakeRelativeUri(uri).ToString(), null);
                return new Uri(ContentUri.BaseUri, relativePath);
            }

            return new Uri(ContentUri.BaseUri, uri.LocalPath.Trim('/'));
        }
    }
}
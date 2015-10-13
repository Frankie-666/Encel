using System;
using System.Collections.Generic;
using System.Linq;
using Encel.Content.Abstractions;
using Encel.Models;

namespace Encel.Content
{
    public static class ContentRepositoryExtensions
    {
        public static IContentData Get(this IContentRepository contentRepository, Uri contentUri)
        {
            return contentRepository.Get<IContentData>(contentUri);
        }

        public static IEnumerable<TContentData> Get<TContentData>(this IContentRepository contentRepository, IEnumerable<Uri> contentUris) where TContentData : class, IContentData
        {
            if (contentUris == null)
                return Enumerable.Empty<TContentData>();

            // TODO: NormalizeContentUri(uri)
            return contentUris.Select(uri => contentRepository.Get<TContentData>(uri));
        }

        public static IEnumerable<TContentData> GetChildren<TContentData>(this IContentRepository contentRepository, Uri contentUri) where TContentData : class, IContentData
        {
            var childrenUris = contentRepository.GetChildrenReferences(contentUri);
            return contentRepository.Get<IContentData>(childrenUris).OfType<TContentData>();
        }

        public static IEnumerable<IContentData> GetChildren(this IContentRepository contentRepository, Uri contentUri)
        {
            return contentRepository.GetChildren<IContentData>(contentUri);
        }

        public static IEnumerable<TContentData> GetDescendants<TContentData>(this IContentRepository contentRepository, Uri contentUri) where TContentData : class, IContentData
        {
            var descendentUris = contentRepository.GetDescendantReferences(contentUri);
            return contentRepository.Get<IContentData>(descendentUris).OfType<TContentData>();
        }

        public static IEnumerable<IContentData> GetDescendants(this IContentRepository contentRepository, Uri contentUri)
        {
            return contentRepository.GetDescendants<IContentData>(contentUri);
        }

        public static IEnumerable<TContentData> GetAncestors<TContentData>(this IContentRepository contentRepository, Uri contentUri) where TContentData : class, IContentData
        {
            var ancestors = contentRepository.GetAncestorReferences(contentUri);
            return contentRepository.Get<IContentData>(ancestors).OfType<TContentData>();
        }

        public static IEnumerable<IContentData> GetAncestors(this IContentRepository contentRepository, Uri contentUri)
        {
            return contentRepository.GetAncestors<IContentData>(contentUri);
        }

        public static IEnumerable<TContentData> GetSiblings<TContentData>(this IContentRepository contentRepository, Uri contentUri) where TContentData : class, IContentData
        {
            var ancestors = contentRepository.GetAncestorReferences(contentUri);
            return contentRepository.Get<IContentData>(ancestors).OfType<TContentData>();
        }

        public static IEnumerable<IContentData> GetSiblings(this IContentRepository contentRepository, Uri contentUri)
        {
            return contentRepository.GetSiblings<IContentData>(contentUri);
        }
    }
}

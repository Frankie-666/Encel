using System;
using System.Collections.Generic;
using Encel.Content.Abstractions;
using Encel.Models;
//using Microsoft.Practices.ServiceLocation;

namespace Encel.Content
{
    public static class ContentDataExtensions
    {
        public static IEnumerable<TContentData> GetChildren<TContentData>(this IContentData contentData) where TContentData : class, IContentData
        {
            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            return contentRepository.GetChildren<TContentData>(contentData.ContentUri);
        }

        public static IEnumerable<TContentData> GetDescendants<TContentData>(this IContentData contentData) where TContentData : class, IContentData
        {
            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            return contentRepository.GetDescendants<TContentData>(contentData.ContentUri);
        }

        public static IEnumerable<TContentData> GetAncestors<TContentData>(this IContentData contentData) where TContentData : class, IContentData
        {
            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            return contentRepository.GetAncestors<TContentData>(contentData.ContentUri);
        }

        public static IEnumerable<TContentData> GetSiblings<TContentData>(this IContentData contentData) where TContentData : class, IContentData
        {
            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            return contentRepository.GetSiblings<TContentData>(contentData.ContentUri);
        }

        public static Uri Save(this IContentData contentData)
        {
            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            return contentRepository.Save(contentData);
        }

        public static Uri Move(this IContentData contentData, Uri parentDestinationUri)
        {
            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            return contentRepository.Move(contentData.ContentUri, parentDestinationUri);
        }

        public static void Delete(this IContentData contentData, bool includeChildren = true)
        {
            var contentRepository = EncelApplication.Configuration.ContentRepositoryFactory.Create();
            contentRepository.Delete(contentData.ContentUri, includeChildren);
        }
    }
}

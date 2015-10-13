using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Encel.Content.Abstractions;

namespace Encel.Content
{
    //https://msdn.microsoft.com/en-us/library/vstudio/ff919782%28v=vs.100%29.aspx
    public class CachedFileContentRepository : FileContentRepository
    {
        public CachedFileContentRepository(IContentPathProvider contentPathProvider)
            : base(contentPathProvider)
        {
        }
        
        public override TContentData Get<TContentData>(Uri contentUri)
        {
            return GetOrSetCache("Get", contentUri.ToString(), () => base.Get<TContentData>(contentUri), ContentUri.GetAbsolutePath(contentUri, true));
        }
        
        public override IEnumerable<Uri> GetChildrenReferences(Uri contentUri)
        {
            return GetOrSetCache("GetChildrenReferences", contentUri.ToString(), () => base.GetChildrenReferences(contentUri));
        }

        public override IEnumerable<Uri> GetDescendantReferences(Uri contentUri)
        {
            return GetOrSetCache("GetDescendantReferences", contentUri.ToString(), () => base.GetDescendantReferences(contentUri));
        }

        public override IEnumerable<Uri> GetAncestorReferences(Uri contentUri)
        {
            return GetOrSetCache("GetAncestorReferences", contentUri.ToString(), () => base.GetAncestorReferences(contentUri));
        }

        public override IEnumerable<Uri> GetSiblingReferences(Uri contentUri)
        {
            return GetOrSetCache("GetSiblingReferences", contentUri.ToString(), () => base.GetSiblingReferences(contentUri));
        }

        private T GetOrSetCache<T>(string prefix, string key, Func<T> setter, string cacheDependencyFilePath = null)
        {
            var memoryCache = MemoryCache.Default;
            key = "Encel::" + prefix + "-" + key;

            if (memoryCache.Contains(key))
            {
                return (T)memoryCache.Get(key);
            }

            var value = setter();

            if (value == null)
                return value;


            var policy = new CacheItemPolicy();

            if (cacheDependencyFilePath != null)
            {
                // TODO
                //var hostFileChangeMonitor = new HostFileChangeMonitor(new List<string> { cacheDependencyFilePath });
                //policy.ChangeMonitors.Add(new SqlChangeMonitor());
            }

            memoryCache.Add(key, value, policy);

            return value;
        }
    }
}

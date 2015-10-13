using Encel.Content.Abstractions;

namespace Encel.Content
{
    public class ContentRepositoryFactory : IContentRepositoryFactory
    {
        public IContentRepository Create()
        {
            var contentPathProvider = EncelApplication.Configuration.ContentPathProvider;

            if (EncelApplication.Configuration.AppSettings.EnableContentCaching)
            {
                return new CachedFileContentRepository(contentPathProvider);
            }

            return new FileContentRepository(contentPathProvider);
        }
    }
}
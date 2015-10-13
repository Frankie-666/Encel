using Encel.Content.Abstractions;

namespace Encel.Content
{
    public class FrontMatterContentSerializerFactory : IContentSerializerFactory
    {
        public IContentSerializer Create()
        {
            var contentTypeProvider = EncelApplication.Configuration.ContentTypeProvider;
            return new FrontMatterContentSerializer(contentTypeProvider);
        }
    }
}
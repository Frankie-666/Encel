 using System;
using System.Collections.Generic;
using Encel.Content;
using Encel.Content.Abstractions;
 using Encel.ContentResolver;
 using Encel.ContentTransformers;
 using Encel.Messaging;
using Encel.Settings;

namespace Encel.Configuration
{
    public class EncelConfiguration
    {
        private static Lazy<EncelConfiguration> _default => new Lazy<EncelConfiguration>(() => new EncelConfiguration().InitializeDefaults());

        public IChannel Channel { get; set; }
        public IContentRepositoryFactory ContentRepositoryFactory { get; set; }
        public IContentSerializerFactory ContentSerializerFactory { get; set; }
        public IContentTypeProvider ContentTypeProvider { get; set; }
        public IContentPathProvider ContentPathProvider { get; set; }
        public List<IContentTransformer> ContentTransformers { get; set; }
        public IContentControllerResolver ContentControllerResolver { get; set; }
        public IEncelAppSettings AppSettings { get; set; }

        public EncelConfiguration()
        {
            ContentTransformers = new List<IContentTransformer>();
            AppSettings = new EncelAppSettings();
        }

        public static EncelConfiguration Default
        {
            get { return _default.Value; }
        }

        public EncelConfiguration InitializeDefaults()
        {
            Channel = new Channel();
            ContentPathProvider = new AppDataContentPathProvider();
            ContentRepositoryFactory = new ContentRepositoryFactory();
            ContentTypeProvider = new ContentTypeProvider();
            ContentSerializerFactory = new FrontMatterContentSerializerFactory();
            ContentControllerResolver = new ContentControllerResolver(new ContentTypeProvider());

            return this;
        }
    }
}

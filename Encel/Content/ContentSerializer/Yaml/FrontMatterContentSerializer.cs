using System;
using System.IO;
using System.Text;
using Encel.Content.Abstractions;
using Encel.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Encel.Content
{
    public class FrontMatterContentSerializer : IContentSerializer
    {
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly Deserializer _yamlDeserializer;
        private readonly Serializer _yamlSerializer;

        public FrontMatterContentSerializer(IContentTypeProvider contentTypeProvider)
        {
            _contentTypeProvider = contentTypeProvider;
            _yamlDeserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention(), ignoreUnmatched: true);
            _yamlSerializer = new Serializer(namingConvention: new CamelCaseNamingConvention());
            _yamlSerializer.RegisterTypeConverter(new DateTimeConverter());
        }

        public string Serialize<TContentData>(TContentData contentData) where TContentData : class, IContentData
        {
            using (var stringWriter = new StringWriter())
            {
                _yamlSerializer.Serialize(stringWriter, contentData);

                return string.Format("---\r\n{0}---\r\n\r\n{1}", stringWriter, contentData.Content);
            }
        }

        public TContentData Deserialize<TContentData>(string filePath) where TContentData : class, IContentData
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                Type contentType;

                // leave stream open so that the next streamReader can read the fileStream.
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    var eventReader = GetEventReader(streamReader);
                    YamlLayoutData layoutData;

                    try
                    {
                        // deserialize to YamlLayoutData so that we can find out which Layout to use.
                        layoutData = _yamlDeserializer.Deserialize<YamlLayoutData>(eventReader);
                    }
                    catch (YamlException ex)
                    {
                        throw new FrontMatterException("YAML Front Matter is missing or could not be parsed from the document: \r\n" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message) + "\r\n\r\nFile: " + filePath);
                    }

                    contentType = layoutData != null && !string.IsNullOrEmpty(layoutData.Layout) ?
                        _contentTypeProvider.GetType(layoutData.Layout) :
                        typeof(ContentData);
                }

                fileStream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(fileStream))
                {
                    var eventReader = GetEventReader(streamReader);

                    var contentData = (TContentData)_yamlDeserializer.Deserialize(eventReader, contentType ?? typeof(ContentData));
                    
                    if (contentData == null)
                    {
                        contentData = (TContentData)(IContentData)new ContentData();
                    }
                    
                    contentData.Content = streamReader.ReadToEnd();

                    return contentData;
                }
            }
        }

        private EventReader GetEventReader(StreamReader streamReader)
        {
            var eventReader = new EventReader(new Parser(streamReader));
            eventReader.Expect<StreamStart>();

            return eventReader;
        }
    }
}
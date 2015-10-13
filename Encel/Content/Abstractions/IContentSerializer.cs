using Encel.Models;

namespace Encel.Content.Abstractions
{
    public interface IContentSerializer
    {
        string Serialize<TContentData>(TContentData contentData) where TContentData : class, IContentData;
        TContentData Deserialize<TContentData>(string filePath) where TContentData : class, IContentData;
    }
}
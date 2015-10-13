using System;

namespace Encel.ContentResolver
{
    public interface IContentControllerResolver
    {
        ContentControllerMetaData Resolve(Type contentType);
    }
}
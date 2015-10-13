using System;
using System.Collections.Generic;

namespace Encel.Content.Abstractions
{
    public interface IContentTypeProvider
    {
        List<Type> GetAllTypes();
        Type GetType(string typeName);
    }
}
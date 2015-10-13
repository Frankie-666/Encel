using System;
using System.Collections.Generic;
using System.Linq;
using Encel.Content.Abstractions;
using Encel.Models;
using Encel.Utilities;

namespace Encel.ContentResolver
{
    public class ContentTypeProvider : IContentTypeProvider
    {
        private static readonly Lazy<List<Type>> _contentTypes = new Lazy<List<Type>>(InitializeTypes);

        public List<Type> GetAllTypes()
        {
            return _contentTypes.Value;
        }

        public Type GetType(string typeName)
        {
            return _contentTypes.Value.FirstOrDefault(type => type.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase));
        }

        private static List<Type> InitializeTypes()
        {
            var contentDataType = typeof(IContentData);
            var reflectionHelper = new ReflectionUtility();
            var contentTypes = reflectionHelper.GetTypesImplementing(contentDataType);

            return contentTypes;
        }
    }
}

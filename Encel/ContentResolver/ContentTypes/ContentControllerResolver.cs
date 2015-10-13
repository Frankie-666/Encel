using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Encel.Content.Abstractions;
using Encel.Utilities;

namespace Encel.ContentResolver
{
    public class ContentControllerResolver : IContentControllerResolver
    {
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly Lazy<List<Type>> _controllers;

        public ContentControllerResolver(IContentTypeProvider contentTypeProvider)
        {
            _contentTypeProvider = contentTypeProvider;
            _controllers = new Lazy<List<Type>>(() => new ReflectionUtility().GetTypesImplementing(typeof(IController)));
        }
        
        public ContentControllerMetaData Resolve(Type contentType)
        {
            var reflectionUtility = new ReflectionUtility();

            foreach (var controller in _controllers.Value)
            {
                var layoutAttributes = reflectionUtility.GetAttributesForType<ContentControllerAttribute>(controller);

                if (layoutAttributes != null)
                {
                    foreach (var layoutAttribute in layoutAttributes)
                    {
                        if (layoutAttribute.ContentType == contentType)
                        {
                            return new ContentControllerMetaData(controller, layoutAttribute.Action);
                        }
                    }
                }
            }

            var nameMatchingControllerType = _controllers.Value.FirstOrDefault(t => t.Name.Equals(string.Concat(contentType.Name, "Controller"), StringComparison.InvariantCultureIgnoreCase));

            if (nameMatchingControllerType == null)
            {
                return null;
            }

            return new ContentControllerMetaData(nameMatchingControllerType);
        }
    }
}

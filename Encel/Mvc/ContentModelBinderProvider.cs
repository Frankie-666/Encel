using System;
using System.Web.Mvc;
using Encel.Models;

namespace Encel.Mvc
{
    public class ContentModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (typeof(IContentData).IsAssignableFrom(modelType))
            {
                return new ContentModelBinder();
            }

            return null;
        }
    }
}
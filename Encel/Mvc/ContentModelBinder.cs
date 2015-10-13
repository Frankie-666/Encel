using System.Web.Mvc;
using Encel.Models;

namespace Encel.Mvc
{
    public class ContentModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelName == "currentContent")
            {
                var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                if (valueProviderResult == null)
                {
                    return null;
                }

                IContentData contentData = valueProviderResult.RawValue as IContentData;

                return contentData;
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
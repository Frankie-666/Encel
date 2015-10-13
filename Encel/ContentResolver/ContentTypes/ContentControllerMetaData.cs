using System;

namespace Encel.ContentResolver
{
    public class ContentControllerMetaData
    {
        public ContentControllerMetaData() { }
        public ContentControllerMetaData(Type controllerType, string actionName = "Index")
        {
            ActionName = actionName;
            ControllerType = controllerType;
            ControllerName = controllerType.Name;

            // remove "Controller" from class name
            if (ControllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
                ControllerName = ControllerName.Substring(0, ControllerName.Length - "Controller".Length);
        }

        public Type ControllerType { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
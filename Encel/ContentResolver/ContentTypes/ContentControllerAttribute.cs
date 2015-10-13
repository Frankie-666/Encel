using System;

namespace Encel.ContentResolver
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ContentControllerAttribute : Attribute
    {        
        public ContentControllerAttribute(Type contentType)
        {
            ContentType = contentType;
            Action = "Index";
        }

        public Type ContentType { get; }

        public string Action { get; set; }
    }
}
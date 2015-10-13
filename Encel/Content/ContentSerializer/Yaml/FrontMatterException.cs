using System;

namespace Encel.Content
{
    [Serializable]
    public class FrontMatterException : Exception
    {
        public FrontMatterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FrontMatterException(string message) : base(message)
        {
        }

        public FrontMatterException()
        {
        }
    }
}
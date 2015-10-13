using System;
using System.Collections.Generic;
using Encel.Models;

namespace Encel.Content.Abstractions
{
    public interface IContentRepository
    {
        TContentData Get<TContentData>(Uri contentUri) where TContentData : class, IContentData;

        IEnumerable<Uri> GetChildrenReferences(Uri contentUri);
        IEnumerable<Uri> GetDescendantReferences(Uri contentUri);
        IEnumerable<Uri> GetAncestorReferences(Uri contentUri);
        IEnumerable<Uri> GetSiblingReferences(Uri contentUri);
        
        Uri Save(IContentData contentData);
        Uri Move(Uri contentUri, Uri parentDestinationUri);
        void Delete(Uri contentUri, bool deleteChildren = true);
    }
}

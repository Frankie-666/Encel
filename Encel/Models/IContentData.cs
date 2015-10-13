using System;

namespace Encel.Models
{
    public interface IContentData
    {
        string Title { get; set; }
        string Description { get; set; }
        string Layout { get; set; }
        string Slug { get; set; }
        Uri ContentUri { get; set; }
        Uri ParentUri { get; set; }
        DateTime Modified { get; set; }
        DateTime Created { get; set; }
        DateTime? Published { get; set; }
        string Name { get; }
        string MenuTitle { get; }
        int? SortIndex { get; set; }
        bool IsDraft { get; set; }
        bool IsPublished { get; }
        
        string Content { get; set; }
    }
}

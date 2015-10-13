using System;
using YamlDotNet.Serialization;

namespace Encel.Models
{
    public class ContentData : IContentData
    {
        [YamlMember(Order = -1)]
        public string Title { get; set; }

        [YamlMember(Order = -1)]
        public string Description { get; set; }

        [YamlMember(Order = -1, Alias = "menu")]
        public string MenuTitle { get; set; }

        [YamlMember(Order = -1)]
        public DateTime Created { get; set; }

        [YamlMember(Order = -1)]
        public DateTime Modified { get; set; }

        [YamlMember(Order = -1)]
        public DateTime? Published { get; set; }

        [YamlMember(Alias = "draft", Order = -1)]
        public bool IsDraft { get; set; }

        [YamlMember(Order = -1)]
        public string Layout { get; set; }


        [YamlMember(Order = -1)]
        public int? SortIndex { get; set; }

        [YamlIgnore]
        public string Slug { get; set; }

        [YamlIgnore]
        public Uri ContentUri { get; set; }

        [YamlIgnore]
        public Uri ParentUri { get; set; }

        [YamlIgnore]
        public string Content { get; set; }

        [YamlIgnore]
        public string Name { get { return Title ?? Slug; } }

        [YamlIgnore]
        public bool IsPublished
        {
            get { return !IsDraft && (!Published.HasValue || Published.Value <= DateTime.UtcNow); }
        }
    }
}

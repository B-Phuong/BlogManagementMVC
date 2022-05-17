
using System.Collections.Generic;

namespace BlogManagement_MVC.Data
{
    public class Tag
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        

        public ICollection<PostTag> PostTags { get; set; }
    }
}

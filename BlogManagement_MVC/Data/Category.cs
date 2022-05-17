
using System;
using System.Collections.Generic;

namespace BlogManagement_MVC.Data
{
    public class Category
    {
        public Int64 Id { get; set; }
        public Int64? ParentID { get; set; }
        public string Title { get; set; }
        public string? MetaTitle { get; set; }
        public string Slug { get; set; }
        public string? Content { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }

        public ICollection<Category> Categories { get; set; }
        public Category CategoryParent { get; set; }
    }
}

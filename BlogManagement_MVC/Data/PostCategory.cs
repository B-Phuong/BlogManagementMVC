
using System;

namespace BlogManagement_MVC.Data
{
    public class PostCategory
    {
        public Int64 PostId { get; set; }
        public Post Post { get; set; }
        public Int64 CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

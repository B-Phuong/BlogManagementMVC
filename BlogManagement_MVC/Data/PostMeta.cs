using System;

namespace BlogManagement_MVC.Data
{
    public class PostMeta
    {
        public Int64 Id { get; set; }
        public string Key { get; set; }
        public string? Content { get; set; }
        public Int64 PostId { get; set; } 
        public Post Post { get; set; }  

    }
}

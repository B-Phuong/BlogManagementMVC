using System;
using System.Collections.Generic;

namespace BlogManagement_MVC.Data
{
    public class PostComment
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }      
        public Byte? Published { get; set; }
        public DateTime CreatedAt { get; set; }    
        public DateTime? PublishedAt { get; set; }
        public string? Content { get; set; }
        public Int64 PostId { get; set; }
        public Post Post { get; set; }

        public Int64? ParentID { get; set; }
        public ICollection<PostComment> Posts { get; set; }
        public PostComment PostParent { get; set; }
        public string AuthorCommentId { get; set; }
        public User AuthorComment { get; set; }


    }
}

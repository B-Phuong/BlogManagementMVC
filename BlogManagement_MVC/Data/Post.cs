
using System;
using System.Collections.Generic;

namespace BlogManagement_MVC.Data
{
    public class Post
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }

        public string ImageURL { get; set; }
        public int TotalVote { get; set; }
        public Byte? Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? Content { get; set; }
        public string AuthorId { get; set; }  //Int64?
        public User Author { get; set; }
        public Int64? ParentID { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Post PostParent { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        public ICollection<PostTag> PostTags { get; set; }

        public ICollection<PostMeta> PostMetas { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; }




    }
}

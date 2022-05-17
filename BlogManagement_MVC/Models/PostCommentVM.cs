using BlogManagement_MVC.Data;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogManagement_MVC.Models
{
    public class PostCommentVM
    {
        public Int64 Id { get; set; }
        [Required(ErrorMessage = "Title is required!")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        public Byte? Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        [Required(ErrorMessage = "Content is required!")]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        public Int64 PostId { get; set; }
        public Post Post { get; set; }
        public Int64? ParentID { get; set; }
        public ICollection<PostComment> Posts { get; set; }
        public string AuthorCommentId { get; set; }
        public User AuthorComment { get; set; }
    }
}

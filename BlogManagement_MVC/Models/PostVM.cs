using BlogManagement_MVC.Data;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogManagement_MVC.Models
{
    public class PostVM
    {
        public Int64 Id { get; set; }
        [Required(ErrorMessage ="Title is required!")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Summary is required!")]
        [DataType(DataType.Text)]
        public string Summary { get; set; }

        public Byte? Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? Content { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public string ImageURL { get; set; }
        public bool IsDeleted { get; set; }
        public IFormFile ImageFile { get; set; }
        public int TotalVote { get; set; }    
    }
}

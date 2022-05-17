using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BlogManagement_MVC.Data
{
    public class User : IdentityUser
    {
       // public Int64 Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
       // public string Mobile { get; set; }
       public string ImageURL { get;set; }
     //   public string Email { get; set; }
     //   public string PasswordHash { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? LastLogIn { get; set; }
        public string? Intro { get; set; }
        public string? Profile { get; set; }
        public ICollection<Post> Posts { get; set; }

        public ICollection<PostComment> PostComments { get;set;}
        public User()
        {
            Posts = new HashSet<Post>();
        }
    }
}

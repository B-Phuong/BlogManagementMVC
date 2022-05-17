using AutoMapper;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<User, UserInfoVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<PostComment, PostCommentVM>().ReverseMap();

        }
    }
}

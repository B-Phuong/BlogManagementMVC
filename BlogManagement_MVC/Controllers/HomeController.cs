using AutoMapper;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostCategoryRepository _postCateRepo;
        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IPostCategoryRepository postCategoryrepo,
            IPostRepository postRepo, IMapper mapper)
        {
            _logger = logger;
            _postCateRepo = postCategoryrepo;
            _postRepo = postRepo;
            _mapper = mapper; 
        }
        [Route("Home/Index/{currentPage:int}")]
        [Route("")]
        public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 4)
        {
            var posts = await _postRepo.FindAll();
            var postList = await _postRepo.GetPostByPaging(currentPage, pageSize); // _repo.FindAll();
            var modelPaging = _mapper.Map<List<Post>, List<PostVM>>(postList.ToList());
            var model = _mapper.Map<List<Post>, List<PostVM>>(posts.ToList());
            if (posts.Count() % pageSize == 0)
            {
                ViewBag.ToTalPage = posts.Count() / pageSize;
            }
            else
            {
                ViewBag.ToTalPage = posts.Count() / pageSize + 1;
            }
            ViewBag.CurrentPage = 1;
            ViewBag.ReturnURL = "/Home/Index";
            if (User.IsInRole("Admin"))
                return View("../Admin/Post/Index", model);
            //var model = _mapper.Map<List<Post>, List<PostCategory>>(posts.ToList());
            return View(modelPaging);
 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

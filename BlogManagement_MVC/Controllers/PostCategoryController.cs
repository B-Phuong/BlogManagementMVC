using AutoMapper;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Controllers
{
    public class PostCategoryController : Controller
    {
        private readonly IPostCategoryRepository _repo;
        private readonly ICategoryRepository _cateRepo;
        private readonly IMapper _mapper;

        public PostCategoryController(IPostCategoryRepository repo, ICategoryRepository cateRepo)
        {
            _repo = repo;
            _cateRepo = cateRepo;


        }
        // GET: PostCategoryController

        public async Task<ActionResult> Index()
        {
            var posts = await _repo.FindAll();

            if (User.IsInRole("Admin"))
                return View("../Admin/Post/Index", posts);
            //var model = _mapper.Map<List<Post>, List<PostCategory>>(posts.ToList());
            return View(posts);
        }


        // GET: PostCategoryController/PostsByCategory/5
        [Route("Category/{id:int}/{currentPage:int}")]
        [Route("Category/{id:int}")]
        public async Task<ActionResult> PostsByCategory(int id, int currentPage = 1, int pageSize = 6)
        {
            var category = await _cateRepo.FindById(id);
            //var postList = await _repo.GetPostCategoryByPaging(currentPage, pageSize); // _repo.FindAll();

            var posts = await _repo.GetPostByCategoryAfterPaging(id, currentPage);// _repo.FindByCategory(id);
           
            if (posts.Count() % pageSize == 0)
            {
                ViewBag.ToTalPage = posts.Count() / pageSize;
            }
            else
            {
                ViewBag.ToTalPage = posts.Count() / pageSize + 1;
            }
            ViewBag.CurrentPage = 1;
            ViewBag.ReturnURL = $"/Category/{id}";
           
            ViewBag.CategoryTitle = category.Title;
            //var model = _mapper.Map<List<Post>, List<PostCategory>>(posts.ToList());
            return View(posts);
        }

        // GET: PostCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Guest")]
        // GET: PostCategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var postDetail = await _repo.FindById(id);           
            return View("../Post/Edit", postDetail);
        }

        // POST: PostCategoryController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      

    }
}

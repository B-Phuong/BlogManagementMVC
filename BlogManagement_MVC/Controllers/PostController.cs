using AutoMapper;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Slugify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _repo;
        private readonly IPostCategoryRepository _categoryRepo;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IMapper _mapper;


        public PostController(IPostRepository postrepo, IMapper mapper,
            IPostCategoryRepository categoryRepo, ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _repo = postrepo;
            _mapper = mapper;
            _context = context;
            _categoryRepo = categoryRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: PostController
        public async Task<ActionResult> Index(int currentPage = 1, int pageSize = 4)
        {
            var postList = await _repo.GetPostByPaging(currentPage, pageSize); // _repo.FindAll();
            var model = _mapper.Map<List<Post>, List<PostVM>>(postList.ToList());
            return View(model);
        }

        // GET: PostController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var postDetail = await _repo.FindById(id);
            ViewBag.CountComment = postDetail.PostComments.Where(p => p.Published == 1).Count();
            var model = _mapper.Map<PostVM>(postDetail);

            return View(model);
        }

        public async Task<ActionResult> GetPostByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postDetail = await _repo.FindAll();
            postDetail = postDetail.Where(p => p.AuthorId == userId).ToList();
            var model = _mapper.Map<List<Post>, List<PostVM>>(postDetail.ToList());

            return View(model);
        }

        // GET: PostController/Create

        [Authorize(Roles = "Guest")]
        public ActionResult Create()
        {
            //ViewBag.CountComment =  _context.Categories.ToList().Where(c=>c.ParentID!=null).Select(c=>c.Title);
            // ViewBag.list = new List<string> { "d", "a" };
            ViewBag.list = new SelectList(_context.Categories.Where(c => c.ParentID != null), "Id", "Title");
            return View();
        }

        // POST: PostController/Create
        [Authorize(Roles = "Guest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostVM post, List<Int64> listCategory)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }
                var newPost = _mapper.Map<Post>(post);
                newPost.AuthorId = userId;
                newPost.CreatedAt = DateTime.Now;
                newPost.Published = 1;
                newPost.Title = post.Title;
                //var Title = post.Title.ToLower().Split(' ');
                //string slug = String.Join('-', Title);
                SlugHelper helper = new SlugHelper();
                string slug = helper.GenerateSlug(post.Title);
                newPost.Content = post.Content;
                newPost.Summary = post.Summary;
                newPost.Slug = slug;
                newPost.MetaTitle = slug;

                if (post.ImageFile != null)
                {
                    IFormFile file = post.ImageFile;
                    string folderPath = "images/";
                    string temp = Guid.NewGuid().ToString();
                    folderPath += temp + "_" + file.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
                    await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    newPost.ImageURL = temp + "_" + post.ImageFile.FileName;
                }
                else
                    newPost.ImageURL = "blog_3.jpg";

                //IFormFile file = post.ImageFile;
                //string folderPath = "images/";
                //string temp = Guid.NewGuid().ToString();
                //folderPath += temp + "_" + file.FileName;
                //string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
                //await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                //newPost.ImageURL = temp + "_" + post.ImageFile.FileName;
                var isSuccess = _repo.Create(newPost).Result;


                foreach (var item in listCategory)
                {
                    var postCategory = new PostCategory();
                    postCategory.Post = newPost;
                    postCategory.CategoryId = item;
                    var isSuccess2 = _categoryRepo.Create(postCategory).Result;
                    if (!isSuccess2)
                    {
                        ModelState.AddModelError("", "Something went wrong when add category for the post");
                        ViewBag.list = new SelectList(_context.Categories.Where(c => c.ParentID != null), "Id", "Title");
                    }
                }
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    ViewBag.list = new SelectList(_context.Categories.Where(c => c.ParentID != null), "Id", "Title");
                    return View(newPost);
                }
                return RedirectToAction("GetPostByUserId"); //RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Guest")]
        // GET: PostController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.list = new SelectList(_context.Categories.Where(c => c.ParentID != null), "Id", "Title");
            var categoryList = _categoryRepo.FindCategoryByPost(id).Result.ToList();
            ViewBag.selectedList = new MultiSelectList(_context.Categories.Where(c => c.ParentID != null), "Id", "Title", categoryList.Select(c => c.Category.Id).ToList());
            //ViewBag.selectedList = categoryList.Select(c => c.Category.Title).ToList();
            var postDetail = await _repo.FindById(id);
            if (userId != postDetail.AuthorId)
                return StatusCode(401, "You cant access to edit this post");
            var model = _mapper.Map<PostVM>(postDetail);
            return View(model);
        }

        // POST: PostController/Edit/5
        [Authorize(Roles = "Guest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostVM post, List<Int64> listCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }
                Post postDetail = await _repo.FindById(Convert.ToInt32(post.Id));
                var updatePost = _mapper.Map<Post>(post);
                updatePost = postDetail;
                updatePost.Summary = post.Summary;
                updatePost.Title = post.Title;

                updatePost.Content = post.Content;
                if (post.ImageFile != null)
                {
                    IFormFile file = post.ImageFile;
                    string folderPath = "images/";
                    string temp = Guid.NewGuid().ToString();
                    folderPath += temp + "_" + file.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
                    await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    updatePost.ImageURL = temp + "_" + post.ImageFile.FileName;
                }


                var isSuccess = await _repo.Update(updatePost);
                if (!isSuccess)
                {

                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(post);
                }
                await _categoryRepo.RemoveAll(await _categoryRepo.FindCategoryByPost(Convert.ToInt32(post.Id)));
                foreach (var item in listCategory)
                {
                    var postCategory = new PostCategory();
                    postCategory.Post = updatePost;
                    postCategory.CategoryId = item;
                    var isSuccess2 = await _categoryRepo.Create(postCategory);
                    if (!isSuccess2)
                    {
                        ModelState.AddModelError("", "Something went wrong when add category for the post");
                        ViewBag.list = new SelectList(_context.Categories.Where(c => c.ParentID != null), "Id", "Title");
                    }
                }
                return RedirectToAction("Details", new { id = post.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        [Authorize(Roles= "Guest, Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);          
            var postDetail = await _repo.FindById(id);
            if (userId != postDetail.AuthorId && !User.IsInRole("Admin"))
                return StatusCode(401, "You cant access to delete this post");
            var model = _mapper.Map<PostVM>(postDetail);
            return View(model);
        }

        // POST: PostController/Delete/5
        [Authorize(Roles = "Guest, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PostVM post)
        {
            try
            {
                Post postDetail = await _repo.FindById(Convert.ToInt32(post.Id));
                var deletePost = _mapper.Map<Post>(post);
                deletePost = postDetail;
                var isSuccess = await _repo.Delete(deletePost);
                var isAdmin = User.IsInRole("Admin");
                if (isSuccess)
                    if(!isAdmin)
                        return RedirectToAction("GetPostByUserId");
                    else
                        return View("~/Views/Admin/Post/Index");
                else return View(deletePost);
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> Private(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postDetail = await _repo.FindById(id);
            if (userId != postDetail.AuthorId)
                return StatusCode(401, "You cant access to private this post");           
            var model = _mapper.Map<PostVM>(postDetail);
            return View(model);
        }

        // POST: PostController/Private/5
        [Authorize(Roles = "Guest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Private(PostVM post)
        {
            try
            {             
                Post postDetail = await _repo.FindById(Convert.ToInt32(post.Id));
                var privatePost = _mapper.Map<Post>(post);
                privatePost = postDetail;
                var isSuccess = await _repo.Private(privatePost);
                return RedirectToAction("GetPostByUserId");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> Publish(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postDetail = await _repo.FindById(id);
            if (userId != postDetail.AuthorId)
                return StatusCode(401, "You cant access to publish this post");         
            var model = _mapper.Map<PostVM>(postDetail);
            return View(model);
        }

        // POST: PostController/Private/5
        [Authorize(Roles = "Guest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Publish(PostVM post)
        {
            try
            {
                Post postDetail = await _repo.FindById(Convert.ToInt32(post.Id));
                var publishPost = _mapper.Map<Post>(post);
                publishPost = postDetail;
                var isSuccess = await _repo.Publish(publishPost);
                return RedirectToAction("GetPostByUserId");
            }
            catch
            {
                return View();
            }
        }
    }
}

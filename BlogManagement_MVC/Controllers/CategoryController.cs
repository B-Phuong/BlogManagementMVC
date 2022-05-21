using AutoMapper;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Slugify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;


        // GET: CategoryController
        public CategoryController(ICategoryRepository categoryrepo, IMapper mapper)
        {
            _repo = categoryrepo;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var category = await _repo.FindAll();
            var model = _mapper.Map<List<Category>, List<CategoryVM>>(category.ToList());
            return View("../Admin/Category/Index", model.Where(c => c.ParentID != null)); //"_Layout",
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            
            ViewBag.list = new SelectList(await _repo.GetCategory(), "Id", "Title");        
            return View("../Admin/Category/Create");
        }

        // POST: CategoryController/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryVM category, Int64 listCategory)
        {
            try
            {               
                ViewBag.list = new SelectList(await _repo.GetCategory(), "Id", "Title");
                var newSubCategory = _mapper.Map<Category>(category);
                newSubCategory.Title = category.Title;
                newSubCategory.ParentID = listCategory;
                var isValid = _repo.CheckValid(newSubCategory).Result;
                if (isValid > 0)
                {
                    ModelState.AddModelError("", "The tittle of sub category has existed!");
                    return View("../Admin/Category/Create", category);
                }
                SlugHelper helper = new SlugHelper();
                string slug = helper.GenerateSlug(category.Title);
                newSubCategory.Content = category.Content;
                newSubCategory.MetaTitle = slug;
                newSubCategory.Slug = slug;
                var isSuccess = _repo.Create(newSubCategory).Result;
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");                 
                    return View("../Admin/Category/Create", category);
                }           
                return RedirectToAction("Index");
            }
            catch
            {
                return View("../Admin/Category/Create");
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            
            var categoryDetail = await _repo.FindById(id);
            ViewBag.list = new SelectList(await _repo.GetCategory(), "Id", "Title", categoryDetail.ParentID);

            var model = _mapper.Map<CategoryVM>(categoryDetail);
            return View("../Admin/Category/Edit", model);
        }


        // POST: CategoryController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryVM category, Int64 listCategory)
        {
            try
            {
                Category categoryDetail = await _repo.FindById(Convert.ToInt32(category.Id));
                var oldTitle = categoryDetail.Title;
                ViewBag.list = new SelectList(await _repo.GetCategory(), "Id", "Title", categoryDetail.ParentID);
                var updateCategory = _mapper.Map<Category>(category);
                updateCategory = categoryDetail;
                //updateCategory.Title = category.Title;
                updateCategory.ParentID = listCategory;
                //var isValid = _repo.CheckValid(updateCategory).Result;
                //if (isValid > 0) && updateCategory.Title != oldTitle)
                //{
                //    ModelState.AddModelError("", "The tittle of sub category has existed!");
                //    return View("../Admin/Category/Edit");
                //}
                //SlugHelper helper = new SlugHelper();
                //string slug = helper.GenerateSlug(category.Title);
                updateCategory.Content = category.Content;
              
                //updateCategory.MetaTitle = slug;
                //updateCategory.Slug = slug;
               
                var isSuccess = await _repo.Update(updateCategory);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View("../Admin/Category/Edit", category);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("../Admin/Category/Edit");
            }
        }

        // GET: CategoryController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [Authorize(Roles = "Admin")]
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

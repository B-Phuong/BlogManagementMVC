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
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [Authorize(Roles = "Admin")]
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
        public async Task<ActionResult> Edit(CategoryVM category)
        {
            try
            {
                Category categoryDetail = await _repo.FindById(Convert.ToInt32(category.Id));
                var updateCategory = _mapper.Map<Category>(category);
                updateCategory = categoryDetail;
                updateCategory.Content = category.Content;
                updateCategory.Title = category.Title;
                updateCategory.ParentID= category.ParentID;
              
                var isSuccess = await _repo.Update(updateCategory);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(category);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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

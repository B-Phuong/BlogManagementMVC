using AutoMapper;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var userList = await _repo.FindAll();
            var model = _mapper.Map<List<User>, List<UserInfoVM>>(userList.ToList());
            return View("../Admin/User/Index", model);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            // var isExists = await _repo.isExists(id);
            //if (!isExists)
            //{
            //    return NotFound();
            //}
            var userInfo = await _repo.FindById(id);
            var model = _mapper.Map<UserInfoVM>(userInfo);
            return View(model);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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

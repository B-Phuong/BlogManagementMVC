using AutoMapper;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Views.Shared.Components.ShowCategory
{
    [ViewComponent]
    public class ShowCategory : ViewComponent
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;


        public ShowCategory(ICategoryRepository categoryrepo, IMapper mapper)
        {
            _repo = categoryrepo;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _repo.FindAll();
            var model = _mapper.Map<List<Category>, List<CategoryVM>>(category.ToList());      
            return View(model); //"_Layout",// Nếu khác Default.cshtml thì trả về View("abc", product) cho abc.cshtml

        }

      
    }
}

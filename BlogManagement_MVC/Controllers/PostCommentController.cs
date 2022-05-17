using AutoMapper;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Controllers
{
    public class PostCommentController : Controller
    {
        private readonly IPostCommentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public PostCommentController(IPostCommentRepository repo, IMapper mapper, IEmailSender emailSender)
        {
            _repo = repo;
            _mapper = mapper;
            _emailSender = emailSender;

        }
        public async Task<IActionResult> FindByPost(int id)
        {
            var commentList = await _repo.FindByPost(id);
            var model = _mapper.Map<List<PostComment>, List<PostCommentVM>>(commentList.ToList());
            ViewBag.PostId = id;
            ViewBag.AuthorCommentList = model.Select(p=>p.AuthorComment).ToList();
            return View("_CommentSection", model);
        }

        public async Task<IActionResult> FindAll(int id)
        {
            var commentList = await _repo.FindAll();
            var model = _mapper.Map<List<PostComment>, List<PostCommentVM>>(commentList.ToList());
            model = model.Where(p => p.PostId == id).ToList();
            ViewBag.PostId = id;
            ViewBag.AuthorCommentList = model.Select(p => p.AuthorComment).ToList();
            return View("_CommentSectionAdminRole", model);
        }
    
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View("../Post/Details");
        }

        // POST: PostController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostCommentVM postComment, IFormCollection form)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var newComment = _mapper.Map<PostComment>(postComment);
                newComment.Content = postComment.Content;
                newComment.AuthorCommentId = userId;
                newComment.CreatedAt = DateTime.Now;
                newComment.PublishedAt = DateTime.Now;
                newComment.Published = 1;
                newComment.Title = postComment.Title;
                var isSuccess = _repo.Create(newComment).Result;
                if(isSuccess)
                {
                    var commentList = await _repo.FindByPost(Convert.ToInt32(postComment.PostId));
                    var author = commentList.FirstOrDefault().Post.Author.Email;
                    await _emailSender.SendEmailAsync(author, $"New comment in '{commentList.FirstOrDefault().Post.Title}'",
                     $"The comment - '{postComment.Content}'");
                }    
                return RedirectToAction("Details","Post", new {id = postComment.PostId});//RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Details","Post", new { id = postComment.PostId });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HiddenComment(PostCommentVM postComment, int commentId)
        {
         
            try
            {
                PostComment postCommentId= await _repo.FindById(commentId);                            
                var updateComment = _mapper.Map<PostComment>(postComment);
                updateComment = postCommentId;
                var isSuccess = await _repo.HiddenComment(updateComment);
                if (isSuccess)
                {

                    return RedirectToAction("Details", "Post", new { id = postComment.PostId });
                }

                return RedirectToAction("Details", "Post", new { id = postComment.PostId });//RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Details", "Post", new { id = postComment.PostId });
            }
        }
    }
}

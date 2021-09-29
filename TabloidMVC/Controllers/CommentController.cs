using System;
using TabloidMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        [Route("comments/{id}")]
        public IActionResult Index(int id)
        {
            var comments = _commentRepository.GetPostComments(id);
            var post = _postRepository.GetPublishedPostById(id);
            var vm = new PostCommentViewModel
            {
                Comments = comments,
                Post = post
            };
            return View(vm);
        }

        public ActionResult Create(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            var vm = new CommentCreateFormViewModel
            {
                Post = post,
                Comment = new Comment()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentCreateFormViewModel commentCreateForm)
        {
            try
            {
                commentCreateForm.Comment.UserProfileId = GetCurrentUserProfileId();
                commentCreateForm.Comment.PostId = id;
                _commentRepository.Add(commentCreateForm.Comment);
                return RedirectToAction("Index", new { id });
            }
            catch(Exception ex)
            {
                var post = _postRepository.GetPublishedPostById(id);
                commentCreateForm.Post = post;
                return View(commentCreateForm);
            }
        }
        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}

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
    }
}

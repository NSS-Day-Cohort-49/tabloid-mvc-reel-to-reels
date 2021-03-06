using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostReactionRepository _postReactionRepository;
        private readonly IReactionRepository _reactionRepository;
        private readonly ITagRepository _tagRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IPostReactionRepository postReactionRepository, IReactionRepository reactionRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _postReactionRepository = postReactionRepository;
            _reactionRepository = reactionRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            var reactions = _reactionRepository.GetAllReactions();
            var postReactions = _postReactionRepository.GetPostReactionsByPostId(id);

            var likes = postReactions.Where(n => n.PostId == id && n.ReactionId == 1);
            var dislikes = postReactions.Where(n => n.PostId == id && n.ReactionId == 2);
            var love = postReactions.Where(n => n.PostId == id && n.ReactionId == 3);

            PostDetailsViewModel vm = new PostDetailsViewModel()
            {
                Reactions = reactions,
                PostReaction = postReactions,
                Post = post,
                PostTags = new List<PostTag>(),
                Likes = likes.Count(),
                Dislikes = dislikes.Count(),
                Love = love.Count()


            };
            return View(vm);
        }

        public IActionResult AddReaction(int id, [FromQuery]int reactionId)
        {
            PostReaction pr = new PostReaction()
            {
                PostId = id,
                ReactionId = reactionId,
                UserProfileId = GetCurrentUserProfileId(),
            };
            
            try
            {
                _postReactionRepository.AddNewReaction(pr);
                return RedirectToAction("Details", new {id});
            }

            catch (Exception ex)
            {
                return StatusCode(500);  
            }
        }

        public IActionResult UserPosts()
        {
            int UserProfileId = GetCurrentUserProfileId();
            var posts = _postRepository.GetUserPosts(UserProfileId);

            return View(posts);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        //GET : PostControler/Delete/5
        public IActionResult Delete(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);
            return View(post);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.DeletePost(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(post);
            }
        }

        //GET : PostController/Edit/5

        public ActionResult Edit (int id)
        {
            int Id = GetCurrentUserProfileId();
            Post post = _postRepository.GetUserPostById(id, Id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            try
            {
                _postRepository.UpdatePost(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        public IActionResult ManageTags(int id)
        {
            PostManageTagsViewModel vm = _postRepository.GetPostWithTags(id);
            vm.PostTags = _tagRepository.GetTagsByPost(id);
            return View(vm);

        }

        public IActionResult AddPostTag(int postId)
        {
            List<Tag> tags = _tagRepository.GetAllTags();
            PostManageTagsViewModel vm = new();
            vm.PostId = postId;
            vm.PostTags = tags;
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddPostTag(PostManageTagsViewModel vm)
        {
            try
            {
                _tagRepository.AddPostTag(vm.TagId, vm.PostId);
                return RedirectToAction("ManageTags", new { id = vm.PostId });
            }
            catch
            {
                return RedirectToAction("ManageTags", new { id = vm.PostId });
            }
        }
        public IActionResult DeletePostTag(int Id, int PostId)
        {
            _tagRepository.DeletePostTag(Id, PostId);
            return RedirectToAction("ManageTags", new { id = PostId });
        }
    }
}
